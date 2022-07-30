using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Commons;
using Backend.ViewModels;
using Response = Backend.Commons.Response;

namespace Backend.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly SignInManager<User> signInManager;

        public AuthController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration
        )
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            var isExist = await userManager.FindByNameAsync(registerVM.Login);
            if (isExist != null)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response { Status = "Error", Message = "User already exists!" }
                );
            
            User user = new User
            {
                UserName = registerVM.Login,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                PasswordHash = registerVM.Password,
                CreatedTime = DateTime.Now,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                NormalizedUserName = registerVM.Login.Trim()
            };
            var result = await userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Response
                    {
                        Status = "Error",
                        Message = "User creation failed! Please check user details and try again."
                    }
                );
            if (!await roleManager.RoleExistsAsync("Default"))
                await roleManager.CreateAsync(new IdentityRole("Default"));
            if (await roleManager.RoleExistsAsync("Default"))
            {
                await userManager.AddToRoleAsync(user, "Default");
            }
            return Ok(new Response { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            //var user1 = await userManager.FindByIdAsync(loginVM.Id);
            var user = await userManager.FindByNameAsync(loginVM.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user, loginVM.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(_configuration["JsonWebTokenKeys:IssuerSigningKey"])
                );

                var token = new JwtSecurityToken(
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(
                        authSigningKey,
                        SecurityAlgorithms.HmacSha256
                    )
                );

                return Ok(
                    new
                    {
                        api_key = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        user = user,
                        Role = userRoles,
                        status = "User Login Successfully"
                    }
                );
            }
            return Unauthorized();
        }
    }
}
