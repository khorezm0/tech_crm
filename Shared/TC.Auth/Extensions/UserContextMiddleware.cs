using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using TC.Auth.Core;
using TC.Auth.Models.Authentication;

namespace TC.Auth.Extensions;

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
            var userId = _authenticationService.GetClaimsUserId(httpContext.User);
            if (userId != null)
            {
                outsourceInfoContext = new UserInfoContext
                {
                    Id = userId.Value 
                };   
            }
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, ex.Message);
            await _next(httpContext);
            return;
        }

        using (_contextAccessor.SetContext(outsourceInfoContext))
        {
            await _next(httpContext);
        }
    }
}