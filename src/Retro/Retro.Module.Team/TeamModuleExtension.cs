using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Retro.Module.Team;

public static class TeamModuleExtension
{
    public static IServiceCollection AddTeamModule(this IServiceCollection services)
    {
        return services;
    }
    
    public static WebApplication AddTeamEndpoints(this WebApplication app)
    {
        return app;
    }
}