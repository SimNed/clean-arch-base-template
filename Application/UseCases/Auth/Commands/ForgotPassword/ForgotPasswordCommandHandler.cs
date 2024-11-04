using Application.Abstractions;
using Application.Abstractions.AuthUser;
using MediatR;

namespace Application.UseCases.Auth.Commands.ForgotPassword
{
    internal sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Result>
    {
        private readonly IAuthService _authService;
        private readonly IAuthUserService _authUserService;

        public ForgotPasswordCommandHandler(IAuthService authService, IAuthUserService userService)
        {
            _authService = authService;
            _authUserService = userService;
        }

        public async Task<Result> Handle(ForgotPasswordCommand command, CancellationToken cancellationToken)
        {
            AuthUserDTO? user = await _authUserService.FindByEmailAsync(command.email);

            if (user == null || await _authUserService.IsEmailConfirmedAsync(command.email))
                return Result.Failure(AuthErrors.EmailNotFound());

            await _authService.SendPasswordResetCodeAsync(command.email);

            return Result.Success();
        }
    }
}
