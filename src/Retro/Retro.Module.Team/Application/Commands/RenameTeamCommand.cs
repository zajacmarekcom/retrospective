using MediatR;

namespace Retro.Module.Team.Application.Commands;

public record RenameTeamCommand(Guid TeamId, string NewName) : IRequest;