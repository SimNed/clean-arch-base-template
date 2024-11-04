using Application.Abstractions;
using Application.Abstractions.AuthUser;
using MediatR;

namespace Application.UseCases.Auth.Commands.ResendConfirmationEmail
{
    internal sealed class ResendConfirmationEmailCommandHandler : IRequestHandler<ResendConfirmationEmailCommand, Result>
    {
        private readonly IAuthService _authService;
        private readonly IAuthUserService _authUserService;

        public ResendConfirmationEmailCommandHandler(IAuthService authService, IAuthUserService userService)
        {
            _authService = authService;
            _authUserService = userService;
        }

        public async Task<Result> Handle(ResendConfirmationEmailCommand command, CancellationToken cancellationToken)
        {
            AuthUserDTO? user = await _authUserService.FindByEmailAsync(command.email);

            if (user == null)
                return Result.Failure(AuthErrors.EmailNotFound());

            else if (await _authUserService.IsEmailConfirmedAsync(command.email))
                return Result.Failure(AuthErrors.EmailAlreadyConfirmed());

            await _authService.SendConfirmationEmailAsync(command.email);

            return Result.Success();  
        }
    }
}
