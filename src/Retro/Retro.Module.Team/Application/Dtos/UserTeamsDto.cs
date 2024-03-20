namespace Retro.Module.Team.Application.Dtos;

public record UserTeamsDto(Guid UserId, IEnumerable<Guid> Teams);