using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Module.User.Infrastructure.Persistence;

namespace Retro.Module.User;

public static class UserModuleExtension
{
    public static IServiceCollection AddUserModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddInfrastructure(configuration);
        return services;
    }
    
    public static IdentityBuilder AddIdentityDatabase(this IdentityBuilder builder)
    {
        builder.AddEntityFrameworkStores<RetroDbContext>();
        return builder;
    }
}