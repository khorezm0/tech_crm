using TC.Business.Abstractions.Users;
using TC.Business.Users;
using TC.DAL.Abstractions.Data;
using TC.DAL.Abstractions.Users;
using TC.DAL.Data;
using TC.DAL.Users;

namespace TC.Api.DependencyInjection;

public static class DefaultBindingsExtension
{
    public static IServiceCollection RegisterDefaultSingletons(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, DbConnectionFactory>();
        services.AddSingleton<IUsersDal, UsersDal>();
        services.AddSingleton<IUsersService, UsersService>();
        return services;
    }
}