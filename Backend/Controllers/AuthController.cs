using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.Models.Commons;
using Backend.Models.Responses.Users;
using Backend.ViewModels;
using Entities;

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
        public async Task<ResponseModel<UserModel>> Register([FromBody] RegisterVM registerVM)
        {
            var isExist = await userManager.FindByNameAsync(registerVM.Login);
            if (isExist != null)
                return new ResponseModel<UserModel>
                {
                    Status = DefaultResponseStatus.BadRequest,
                    Message = "User already exists!"
                };

            var user = new User(
                null,
                registerVM.Login?.Trim(),
                registerVM.PhoneNumber,
                false,
                registerVM.Email,
                false,
                registerVM.Password,
                registerVM.FirstName,
                registerVM.LastName,
                DateTime.Now,
                null
            );
            var result = await userManager.CreateAsync(user, registerVM.Password);
            if (!result.Succeeded)
                return new ResponseModel<UserModel>
                {
                    Status = DefaultResponseStatus.Fail,
                    Message = "User creation failed! Please check user details and try again."
                };
            if (!await roleManager.RoleExistsAsync(Common.Enums.UserRole.Default.ToString()))
                await roleManager.CreateAsync(
                    new IdentityRole(Common.Enums.UserRole.Default.ToString())
                );
            if (await roleManager.RoleExistsAsync(Common.Enums.UserRole.Default.ToString()))
            {
                await userManager.AddToRoleAsync(user, Common.Enums.UserRole.Default.ToString());
            }

            return new ResponseModel<UserModel>
            {
                Status = DefaultResponseStatus.Ok,
                Message = "User created successfully!"
            };
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ResponseModel<AuthorizationModel>> Login([FromBody] LoginVM loginVM)
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

                return new ResponseModel<AuthorizationModel>()
                {
                    Status = DefaultResponseStatus.Ok,
                    Data = new AuthorizationModel()
                    {
                        ApiKey = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        User = UserModel.FromEntity(user),
                        Role = userRoles.FirstOrDefault() ?? string.Empty,
                    }
                };
            }

            return new ResponseModel<AuthorizationModel>() { Status = DefaultResponseStatus.Fail };
        }
    }
}
