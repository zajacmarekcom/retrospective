using Microsoft.EntityFrameworkCore;

namespace Retro.Module.Team.Infrastructure.Persistence;

public class TeamDbContext(DbContextOptions<TeamDbContext> options) : DbContext(options)
{
    
}