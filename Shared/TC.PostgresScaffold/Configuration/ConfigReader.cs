using Microsoft.Extensions.Configuration;

namespace TC.PostgresScaffold.Configuration;

public class ConfigReader
{
    public ConfigurationOptions Read(IConfiguration configuration)
    {
        var config = new ConfigurationOptions();
        config.Scaffold = configuration.GetSection("Scaffold").Get<ScaffoldOptions>();
        config.ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
        return config;
    }
}