using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TC.Auth.Models.Tokens;

namespace TC.Auth.Extensions;
public static class IdentityServiceExtenstion
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
    
        var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    
        var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
    
        services.AddSingleton(signingConfigurations);
    
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(jwtBearerOptions =>
            {
                jwtBearerOptions.SaveToken = true;
                jwtBearerOptions.RequireHttpsMetadata = false;
                jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = signingConfigurations.SecurityKey,
                    ClockSkew = TimeSpan.Zero
                };
            });
        
        return services;
    }
}
