using Retro.Module.Team.Application.Dtos;

namespace Retro.Front.Interfaces;

public interface ITeamService
{
    Task<Guid> CreateTeam(string name);
    Task AddUserToTeam(string teamId, string userId);
    Task RemoveUserFromTeam(string teamId, string userId);
    Task<IEnumerable<TeamDto>> GetTeams();
    Task<TeamDto?> GetTeam(string teamId);
}