using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Module.Team.Domain;
using Retro.Module.Team.Infrastructure.Persistence;

namespace Retro.Module.Team;

public static class TeamModuleExtension
{
    public static IServiceCollection AddTeamModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<ITeamRepository, TeamRepository>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(TeamModuleExtension).Assembly));
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<TeamDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}