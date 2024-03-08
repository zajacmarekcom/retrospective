using Retro.Module.Team.Application.Dtos;

namespace Retro.Front.Components.Common.Services;

public class TeamDataService
{
    public TeamDto? CurrentTeam { get; private set; }

    public async Task SetTeam(TeamDto team)
    {
        CurrentTeam = team;
        TeamChanged?.Invoke(team);
    }
    
    public event TeamChangedHandler? TeamChanged;
    public delegate void TeamChangedHandler(TeamDto? team);
}