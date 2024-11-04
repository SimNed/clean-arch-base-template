using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignUp
{
    public sealed record SignUpCommand(string username, string email, string password) : IRequest<Result<SignUpCommandResponse?>>;
}
