namespace Retro.Module.Team.Application.Dtos;

public record TeamDto(Guid Id, string Name, IEnumerable<TeamUserDto> Users);