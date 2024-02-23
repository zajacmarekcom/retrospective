using MediatR;
using Retro.Common.Results;

namespace Retro.Application.Account.Commands;

public record CreateUserCommand(string Email, string Password) : IRequest<Result>;