using System.Security.Claims;
using MediatR;
using Retro.Front.Interfaces;
using Retro.Module.Team.Application.Commands;
using Retro.Module.Team.Application.Dtos;
using Retro.Module.Team.Application.Queries;

namespace Retro.Front.Services;

public class TeamService(ISender mediator, IHttpContextAccessor contextAccessor) : ITeamService
{
    
    public async Task<Guid> CreateTeam(string name)
    {
        var userId = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var command = new CreateTeamCommand(name, Guid.Parse(userId));
        return await mediator.Send(command);
    }

    public Task AddUserToTeam(string teamId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserFromTeam(string teamId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TeamDto>> GetTeams()
    {
        throw new NotImplementedException();
    }

    public async Task<TeamDto?> GetTeam(string teamId)
    {
        var userId = contextAccessor.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var query = new GetTeamQuery(Guid.Parse(teamId), Guid.Parse(userId));
        
        return await mediator.Send(query);
    }
}