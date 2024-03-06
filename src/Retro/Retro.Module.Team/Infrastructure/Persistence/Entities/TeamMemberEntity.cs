namespace Retro.Module.Team.Infrastructure.Persistence.Entities;

public class TeamMemberEntity
{
    public Guid Id { get; set; }
    public Guid TeamId { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset ValidFrom { get; set; }
    public DateTimeOffset? ValidTo { get; set; }
}