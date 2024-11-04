using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.ForgotPassword
{
    public sealed record ForgotPasswordCommand(string email) : IRequest<Result>;
}
