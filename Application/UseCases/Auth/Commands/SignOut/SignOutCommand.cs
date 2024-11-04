using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignOut
{
    public sealed record SignOutCommand() : IRequest<Result>;
}
