using Microsoft.AspNetCore.Mvc;
using TC.Api.ClientData.Auth;
using TC.Api.ClientData.Commons;
using TC.Api.ClientData.Users;
using TC.Auth.Core;
using TC.Auth.Security.Hashing;
using TC.Business.Abstractions.Users;
using TC.Business.Abstractions.Users.Models;

namespace TC.Api.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IAuthenticationService _authenticationService;


        public AuthController(
            IUsersService usersService,
            IAuthenticationService authenticationService,
            IPasswordHasher passwordHasher
        )
        {
            _passwordHasher = passwordHasher;
            _usersService = usersService;
            _authenticationService = authenticationService;
        }

        [HttpPost("Register")]
        [ProducesResponseType(typeof(ApiBaseResponse<UserDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterPostRequest registerPostRequest)
        {
            registerPostRequest.Login = registerPostRequest.Login.Trim();

            var user = registerPostRequest.Map(_passwordHasher.HashPassword(registerPostRequest.Password));
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
        public async Task<IActionResult> LoginAsync([FromBody] LoginPostRequest request)
        {
            var user = await _usersService.GetByUserNameAsync(request.UserName.Trim());
            if (user == null)
            {
                return new ApiDataResult(new ApiResponse("User not found"), StatusCodes.Status401Unauthorized);
            }

            var tokenInfo = _authenticationService
                .CreateAccessToken(user.MapToAuthModel(), request.Password);
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
                    User = user.Map(),
                    Role = user.Roles?.FirstOrDefault()?.ToString() ?? string.Empty,
                }
            });
        }

        [HttpPost("Token/Refresh")]
        [ProducesResponseType(typeof(ApiBaseResponse<AuthorizationDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshTokenAsync(
            [FromBody] RefreshTokenPostDto refreshTokenPostDto)
        {
            var userModel = await _usersService.GetByIdAsync(refreshTokenPostDto.UserId);
            var response = _authenticationService
                .RefreshToken(userModel.MapToAuthModel(), refreshTokenPostDto.Token);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            return new ApiDataResult(new ApiBaseResponse<AuthorizationDto>
            {
                Data = new AuthorizationDto
                {
                    ApiKey = response.Token.Token,
                    Expiration = response.Token.Expiration,
                    User = userModel.Map(),
                    Role = userModel.Roles?.FirstOrDefault()?.ToString() ?? string.Empty,
                }
            });
        }

        [HttpPost("Token/Revoke")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RevokeTokenAsync([FromBody] RevokeTokenPostDto postDto)
        {
            var userModel = await _usersService.GetByIdAsync(postDto.UserId);
            _authenticationService.RevokeRefreshToken(userModel.MapToAuthModel(), postDto.Token);
            return NoContent();
        }
    }
}