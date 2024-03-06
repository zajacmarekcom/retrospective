using MediatR;
using Retro.Module.Team.Application.Dtos;

namespace Retro.Module.Team.Application.Queries;

public record GetTeamQuery(Guid TeamId, Guid UserId) : IRequest<TeamDto?>;