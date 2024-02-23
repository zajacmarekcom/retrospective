using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Infrastructure.Persistence;

namespace Retro.Infrastructure;

public static class InfrastructureServiceExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<RetroDbContext>(options =>
            options.UseSqlServer(connectionString));
        
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(InfrastructureServiceExtension).Assembly));

        return services;
    }
    
    public static IdentityBuilder AddIdentityDatabase(this IdentityBuilder builder)
    {
        builder.AddEntityFrameworkStores<RetroDbContext>();
        return builder;
    }
}