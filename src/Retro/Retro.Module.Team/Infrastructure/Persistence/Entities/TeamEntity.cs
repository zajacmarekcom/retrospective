namespace Retro.Module.Team.Infrastructure.Persistence.Entities;

public class TeamEntity
{
    public Guid Id { get; set; }
    public Guid OwnerId { get; set; }
    public string Name { get; set; }
}