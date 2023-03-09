using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TC.AspNetCore.Configurations;

public static class ConfigureOptionsExtension
{
    public static void ConfigureTcOptions(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ConnectionStrings>(configuration.GetSection("ConnectionStrings"));
    }
}