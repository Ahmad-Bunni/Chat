using ChatApp.Data.Repositories;
using ChatApp.Domain.Interfaces;
using ChatApp.Domain.Serivces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApp.Shared.DI;
public static class DependencyManager
{
    public static void RegisterDependencies(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));

        services.AddScoped(typeof(IUserService), typeof(UserService));
    }
}
