using Retro.Common.Domain;
using Retro.Module.Team.Application.Dtos;

namespace Retro.Module.Team.Domain;

public interface ITeamRepository : IRepository<Team>
{
    Task<UserTeamsDto> GetByUserIdAsync(Guid userId);
}