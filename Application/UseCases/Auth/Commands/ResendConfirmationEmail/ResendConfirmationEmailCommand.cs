using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.ResendConfirmationEmail
{
    public sealed record ResendConfirmationEmailCommand(string email) : IRequest<Result>;
}
