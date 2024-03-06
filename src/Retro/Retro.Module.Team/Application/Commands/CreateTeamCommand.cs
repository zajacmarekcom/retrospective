using MediatR;

namespace Retro.Module.Team.Application.Commands;

public record CreateTeamCommand(string Name, Guid OwnerId) : IRequest<Guid>;