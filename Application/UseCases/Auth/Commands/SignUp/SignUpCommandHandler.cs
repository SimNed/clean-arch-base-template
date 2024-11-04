using Application.Abstractions;
using Application.Abstractions.AuthUser;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignUp
{
    internal sealed class SignUpCommandHandler : IRequestHandler<SignUpCommand, Result<SignUpCommandResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IAuthUserService _userService;

        public SignUpCommandHandler(IAuthService authRepository, IAuthUserService userService)
        {
            _authService = authRepository;
            _userService = userService;
        }

        public async Task<Result<SignUpCommandResponse>> Handle(SignUpCommand command, CancellationToken cancellationToken)
        {
            if (!await _userService.IsUsernameAvailable(command.username))
                return Result<SignUpCommandResponse>.Failure(AuthErrors.ExistingUsername());

            SignUpCommandResponse? response = await _authService.SignUpAsync(command.username, command.email, command.password);

            if (response == null)
                return Result<SignUpCommandResponse>.Failure(AuthErrors.RegistrationFailed());

            await _authService.SendConfirmationEmailAsync(command.email);

            return Result<SignUpCommandResponse>.Success(response);
        }
    }
}
