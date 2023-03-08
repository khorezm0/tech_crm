using Authentication.Core;
using Backend.ClientData.Auth;
using Backend.ClientData.Commons;
using Backend.ClientData.Users;
using Business.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[Route("v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiController]
[Authorize]
public class UserController : ControllerBase
{
    private readonly UsersService _usersService;
    private readonly IUserContextAccessor _userContextAccessor;

    public UserController(
        UsersService usersService, IUserContextAccessor userContextAccessor)
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