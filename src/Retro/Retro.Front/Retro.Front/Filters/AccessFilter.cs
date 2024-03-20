using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Retro.Module.Team.Domain;
using Retro.Module.User.Account;

namespace Retro.Front.Filters;

public class AccessFilter(ITeamRepository teamRepository, UserManager<ApplicationUser> userManager) : IActionFilter, IEndpointFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        return;
    }

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var user = context.HttpContext.User;
        var teamId = context.HttpContext.GetRouteData().Values["teamId"];
        var userTeams = await teamRepository.GetByUserIdAsync(Guid.Parse(userManager.GetUserId(user)!));
        
        if (!userTeams.Teams.Contains(Guid.Parse(teamId?.ToString() ?? string.Empty)))
        {
            return Results.Forbid();
        }
        
        return await next(context);
    }
}