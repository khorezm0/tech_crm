using Authentication.Core;
using Authentication.Core.Security.Hashing;
using Authentication.Security.Hashing;
using Authentication.Security.Tokens;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication.Extensions;
public static class MvcJwtExtenstion
{
    public static IServiceCollection AddJwt(
        this IServiceCollection services)
    {
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
    
        services.AddSingleton<ITokenHandler, TokenHandler>();
    
        services.AddScoped<IAuthenticationService, AuthenticationService>();
    
        return services;
    }
}
