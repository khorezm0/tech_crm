using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Authentication.Core;
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
        private readonly IAuthenticationService _authenticationService;

        
        public AuthController(
            UsersService usersService,
            IConfiguration configuration,
            IAuthenticationService authenticationService
        )
        {
            _configuration = configuration;
            _usersService = usersService;
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(ApiBaseResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterPostRequest registerPostRequest)
        {
            registerPostRequest.Login = registerPostRequest.Login.Trim();

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
                return new ApiDataResult(new ApiResponse
                {
                    Message = "Unknown exception occured!",
                }, StatusCodes.Status400BadRequest);
            }
            
            return new ApiDataResult(new ApiBaseResponse<UserDto>
            {
                Message = "User created successfully!"
            });
        }

        [HttpPost("Login")]
        [ProducesResponseType(typeof(ApiBaseResponse<AuthorizationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginPostRequest request)
        {
            var user = await _usersService.GetByUserNameAsync(request.UserName.Trim());
            if (user == null)
            {
                return new ApiDataResult(new ApiResponse("User not found"), StatusCodes.Status401Unauthorized);
            }
            
            var tokenInfo = _authenticationService
                .CreateAccessTokenAsync(user.Map(), request.Password);
            if (!tokenInfo.Success || tokenInfo.Token == null)
            {
                return new ApiDataResult(new ApiResponse(tokenInfo.Message), StatusCodes.Status401Unauthorized);
            }
            
            return new ApiDataResult(new ApiBaseResponse<AuthorizationDto>
            {
                Data = new AuthorizationDto
                {
                    ApiKey = tokenInfo.Token.Token,
                    Expiration = tokenInfo.Token.Expiration,
                    User = UserDto.Map(user),
                    Role = user.Roles.FirstOrDefault()?.ToString() ?? string.Empty,
                }
            });
        }
    }
}