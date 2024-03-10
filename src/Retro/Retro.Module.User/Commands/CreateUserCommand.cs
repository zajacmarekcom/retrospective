using MediatR;
using Retro.Common.Results;

namespace Retro.Module.User.Commands;

public record CreateUserCommand(string Email, string Password) : IRequest<Result>;