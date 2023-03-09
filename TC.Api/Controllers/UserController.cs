using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TC.Api.ClientData.Auth;
using TC.Api.ClientData.Commons;
using TC.Api.ClientData.Users;
using TC.Auth.Core;
using TC.Business.Abstractions.Users;

namespace TC.Api.Controllers;

[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly IUsersService _usersService;
    private readonly IUserContextAccessor _userContextAccessor;

    public UserController(
        IUsersService usersService,
        IUserContextAccessor userContextAccessor)
    {
        _usersService = usersService;
        _userContextAccessor = userContextAccessor;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiBaseResponse<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAsync()
    {
        var userId = _userContextAccessor.Info.Id;
        var user = await _usersService.GetByIdAsync(userId);
        return new ApiDataResult(new ApiBaseResponse<UserDto>()
        {
            Data = user.MapToDto()
        });
    }
}