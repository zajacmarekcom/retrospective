using MediatR;
using Retro.Module.Team.Application.Commands;
using Retro.Module.Team.Domain;

namespace Retro.Module.Team.Infrastructure.Handlers;

public class CreateTeamCommandHandler(ITeamRepository teamRepository) : IRequestHandler<CreateTeamCommand, Guid>
{
    public async Task<Guid> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        var team = new Domain.Team(request.OwnerId ,request.Name);
        await teamRepository.AddAsync(team);
        
        return team.Id;
    }
}