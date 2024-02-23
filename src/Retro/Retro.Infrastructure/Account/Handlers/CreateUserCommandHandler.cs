using MediatR;
using Microsoft.AspNetCore.Identity;
using Retro.Application.Account.Commands;
using Retro.Common.Results;

namespace Retro.Infrastructure.Account.Handlers;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserStore<ApplicationUser> _userStore;

    public CreateUserCommandHandler(UserManager<ApplicationUser> userManager, IUserStore<ApplicationUser> userStore)
    {
        _userManager = userManager;
        _userStore = userStore;
    }

    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = _userManager.CreateAsync(user, request.Password).Result;
        
        return result.Succeeded
            ? new Result(true)
            : new Result(false, result.Errors.Select(e => new Error(e.Description)));
    }
}