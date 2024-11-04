using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignIn
{
    public sealed record SignInCommand(string username, string password) : IRequest<Result<SignInCommandResponse>>;
}
