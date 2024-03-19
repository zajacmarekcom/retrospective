using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Retro.Module.Board.Infrastructure.Persistence;

namespace Retro.Module.Board;

public static class BoardModuleExtension
{
    public static IServiceCollection AddBoardModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(BoardModuleExtension).Assembly));
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<BoardDbContext>(options =>
            options.UseSqlServer(connectionString));

        return services;
    }
}