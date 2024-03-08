using Retro.Front.Interfaces;
using Retro.Module.Team.Application.Dtos;

namespace Retro.Front.Components.Common.Services;

public class TeamDataService(ITeamService teamService)
{
    private Guid? _currentTeamId;
    public TeamDto? CurrentTeam { get; private set; }

    public async Task SetTeam(Guid teamId)
    {
        _currentTeamId = teamId;
        CurrentTeam = await teamService.GetTeam(teamId.ToString());
    }
}