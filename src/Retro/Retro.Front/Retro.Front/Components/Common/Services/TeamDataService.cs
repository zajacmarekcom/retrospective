using Blazored.LocalStorage;
using Retro.Module.Team.Application.Dtos;

namespace Retro.Front.Components.Common.Services;

public class TeamDataService(ILocalStorageService localStorage)
{
    public TeamDto? CurrentTeam { get; private set; }

    public async Task SetTeam(TeamDto team)
    {
        CurrentTeam = team;
        await localStorage.SetItemAsync("team", team);
        TeamChanged?.Invoke(team);
    }
    
    public async Task Initialize()
    {
        CurrentTeam = await localStorage.GetItemAsync<TeamDto>("team");
        TeamChanged?.Invoke(CurrentTeam);
    }
    
    public event TeamChangedHandler? TeamChanged;
    public delegate void TeamChangedHandler(TeamDto? team);
}