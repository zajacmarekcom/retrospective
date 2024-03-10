using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Module.User.Infrastructure.Persistence;
using Retro.Module.User.Persistence;

namespace Retro.Module.User;

internal static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<RetroDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InfrastructureServiceExtension).Assembly));

        return services;
    }
}