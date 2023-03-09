using Microsoft.Extensions.DependencyInjection;
using TC.Auth.Core;
using TC.Auth.Security.Hashing;
using TC.Auth.Security.Tokens;

namespace TC.Auth.Extensions;
public static class MvcJwtExtenstion
{
    public static IServiceCollection AddJwt(
        this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    
        services.AddSingleton<ITokenHandler, TokenHandler>();
    
        services.AddSingleton<IUserContextAccessor, UserContextAccessor>();

        services.AddSingleton<IAuthenticationService, AuthenticationService>();
    
        return services;
    }
}
