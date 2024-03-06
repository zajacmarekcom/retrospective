using MediatR;
using Retro.Module.Team.Application.Dtos;
using Retro.Module.Team.Application.Queries;
using Retro.Module.Team.Infrastructure.Persistence;

namespace Retro.Module.Team.Infrastructure.Handlers.Queries;

public class GetTeamQueryHandler(TeamDbContext dbContext) : IRequestHandler<GetTeamQuery, TeamDto?>
{
    public async Task<TeamDto?> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        var team = dbContext.Teams
            .FirstOrDefault(t => t.Id == request.TeamId);

        if (team is null)
        {
            return null;
        }
        
        var members = dbContext.Members
            .Where(tu => tu.TeamId == request.TeamId && tu.ValidTo == null)
            .Select(tu => new TeamUserDto(tu.UserId, false))
            .ToList();
        members.Add(new TeamUserDto(team.OwnerId, true));
        
        if (members.All(m => m.Id != request.UserId))
        {
            return null;
        }
        
        return new TeamDto(team.Id, team.Name, members);
    }
}