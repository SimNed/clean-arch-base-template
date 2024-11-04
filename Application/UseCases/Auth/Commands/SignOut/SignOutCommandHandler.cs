using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignOut
{
    internal sealed class SignOutCommandHandler : IRequestHandler<SignOutCommand, Result>
    {
        private readonly IAuthService _authService;

        public SignOutCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<Result> Handle(SignOutCommand command, CancellationToken cancellationToken)
        {
            await _authService.SignOutAsync();

            return Result.Success();
        }
    }
}
