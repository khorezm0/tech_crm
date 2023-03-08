using System.Net;
using Authentication.Core;
using Authentication.Models.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Authentication.Extensions;

public class UserContextMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IUserContextAccessor _contextAccessor;
    private readonly IAuthenticationService _authenticationService;

    public UserContextMiddleware(
        RequestDelegate next,
        ILogger<UserContextMiddleware> logger,
        IUserContextAccessor contextAccessor, IAuthenticationService authenticationService)
    {
        _next = next;
        _logger = logger;
        _contextAccessor = contextAccessor;
        _authenticationService = authenticationService;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        UserInfoContext outsourceInfoContext = null;

        try
        {
            outsourceInfoContext = new UserInfoContext
            {
                Id = _authenticationService.GetClaimsUserId(httpContext.User)
            };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, ex.Message);
            return;
        }

        using (_contextAccessor.SetContext(outsourceInfoContext))
        {
            await _next(httpContext);
        }
    }
}