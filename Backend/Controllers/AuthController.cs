using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Backend.ClientData.Auth;
using Backend.ClientData.Commons;
using Backend.ClientData.Users;
using Business.Abstractions;
using Business.Services;

namespace Backend.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UsersService _usersService;
        
        public AuthController(
            UsersService usersService,
            IConfiguration configuration
        )
        {
            _configuration = configuration;
            _usersService = usersService;
        }

        [HttpPost("Register")]
        public async Task<ApiResponse<UserDto>> Register([FromBody] RegisterPostRequest registerPostRequest)
        {
            registerPostRequest.Login = registerPostRequest.Login.Trim();
            
            var isExist = await _usersService.FindByNameAsync(registerPostRequest.Login);
            if (isExist != null)
                return new ApiResponse<UserDto>
                {
                    Status = DefaultResponseStatus.BadRequest,
                    Message = "User already exists!"
                };

            var user = new User
            {
                UserName = registerPostRequest.Login?.Trim(),
                PhoneNumber = registerPostRequest.PhoneNumber,
                Email = registerPostRequest.Email,
                PasswordHash = registerPostRequest.Password,
                FirstName = registerPostRequest.FirstName,
                LastName = registerPostRequest.LastName,
                CreatedTime = DateTime.Now,
            };

            var result = await _usersService.AddAsync(user);
            if (result == null)
            {
                return new ApiResponse<UserDto>
                {
                    Status = DefaultResponseStatus.Fail,
                    Message = "Unknown exception occured!"
                };
            }
            
            return new ApiResponse<UserDto>
            {
                Status = DefaultResponseStatus.Ok,
                Message = "User created successfully!"
            };
        }

        [HttpPost("Login")]
        public async Task<ApiResponse<AuthorizationDto>> Login([FromBody] LoginPostRequest loginPostRequest)
        {
            //TODO: create JWT
            var user = await userManager.FindByNameAsync(loginPostRequest.UserName);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginPostRequest.Password))
                return new ApiResponse<AuthorizationDto> { Status = DefaultResponseStatus.Fail };

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var identity = new ClaimsIdentity
            {
            };

            identity.AddClaims(authClaims.Concat(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole))));

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

            return new ApiResponse<AuthorizationDto>
            {
                Status = DefaultResponseStatus.Ok,
                Data = new AuthorizationDto
                {
                    ApiKey = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    User = UserDto.Map(user),
                    Role = userRoles.FirstOrDefault() ?? string.Empty,
                }
            };
        }

        [HttpPost("Telegram")]
        public async Task<ApiResponse<AuthorizationDto>> TelegramLogin([FromQuery] TelegramAuthGetRequest request)
        {
            var user = await userManager.FindByIdAsync(request.UserId);
            if (user == null || !await userManager.CheckPasswordAsync(user, loginPostRequest.Password))
                return new ApiResponse<AuthorizationDto> { Status = DefaultResponseStatus.Fail };

            var userRoles = await userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            authClaims.AddRange(userRoles.Select(userRole => new Claim(ClaimTypes.Role, userRole)));

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

            return new ApiResponse<AuthorizationDto>
            {
                Status = DefaultResponseStatus.Ok,
                Data = new AuthorizationDto
                {
                    ApiKey = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    User = UserDto.Map(user),
                    Role = userRoles.FirstOrDefault() ?? string.Empty,
                }
            };
        }
    }
}