﻿namespace Authentication.Extensions;
public static class IdentityServiceExtenstions
{
    // public static IServiceCollection AddIdentityServices(
    //     this IServiceCollection services,
    //     IConfiguration configuration)
    // {
    //     services.Configure<TokenOptions>(configuration.GetSection("TokenOptions"));
    //
    //     var tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();
    //
    //     var signingConfigurations = new SigningConfigurations(tokenOptions.Secret);
    //
    //     services.AddSingleton(signingConfigurations);
    //
    //     services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    //         .AddJwtBearer(jwtBearerOptions =>
    //         {
    //             jwtBearerOptions.TokenValidationParameters = new TokenValidationParameters()
    //             {
    //                 ValidateAudience = true,
    //                 ValidateLifetime = true,
    //                 ValidateIssuerSigningKey = true,
    //                 ValidIssuer = tokenOptions.Issuer,
    //                 ValidAudience = tokenOptions.Audience,
    //                 IssuerSigningKey = signingConfigurations.SecurityKey,
    //                 ClockSkew = TimeSpan.Zero
    //             };
    //         });
    //
    //     return services;
    // }
}
