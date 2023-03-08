using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TC.AspNetCore.DependencyInjection;

public static class MvcDiExtension
{
    public static void RegisterByDiAttribute(this IServiceCollection services, string assemblySearchPattern)
    {
        var assemblyPath = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty).LocalPath;
        services.RegisterByDiAttribute(assemblyPath, assemblySearchPattern);
    }
    
    public static void RegisterByDiAttribute(this IServiceCollection services, string assemblyPath, string assemblySearchPattern)
    {
        var installer = new DiInstaller(services);
        installer.RegisterByDIAttribute(assemblyPath, assemblySearchPattern);
        installer.Initialize();
    }
}