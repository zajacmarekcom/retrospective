using MediatR;
using Microsoft.AspNetCore.Mvc;
using Retro.Front.Filters;
using Retro.Module.Team.Application.Commands;

namespace Retro.Front.Endpoints;

public static class TeamEndpoints
{
    public static IEndpointRouteBuilder AddTeamEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/team");

        group.MapPost("/",
            ([FromBody] CreateTeamCommand command, [FromServices] IMediator mediator) => mediator.Send(command));
        group.MapPut("/{teamId}",
            ([FromBody] RenameTeamCommand command, [FromRoute] Guid teamId, [FromServices] IMediator mediator) =>
                mediator.Send(command with { TeamId = teamId }))
            .AddEndpointFilter<AccessFilter>();

        return app;
    }
}