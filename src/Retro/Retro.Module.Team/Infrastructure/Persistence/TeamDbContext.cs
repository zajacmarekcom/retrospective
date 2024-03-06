using Microsoft.EntityFrameworkCore;
using Retro.Module.Team.Infrastructure.Persistence.Entities;

namespace Retro.Module.Team.Infrastructure.Persistence;

public class TeamDbContext(DbContextOptions<TeamDbContext> options) : DbContext(options)
{
    public required DbSet<TeamEntity> Teams { get; set; }
    public required DbSet<TeamMemberEntity> Members { get; set; }
}