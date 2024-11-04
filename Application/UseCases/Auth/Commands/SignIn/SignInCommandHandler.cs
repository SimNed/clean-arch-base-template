using Application.Abstractions;
using Application.Abstractions.AuthUser;
using MediatR;

namespace Application.UseCases.Auth.Commands.SignIn
{
    internal sealed class SignInCommandHandler : IRequestHandler<SignInCommand, Result<SignInCommandResponse>>
    {
        private readonly IAuthService _authService;
        private readonly IAuthUserService _userService;

        public SignInCommandHandler(IAuthService authService, IAuthUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public async Task<Result<SignInCommandResponse>> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            AuthUserDTO? user = await _userService.FindByUserNameAsync(command.username);

            if (user == null)
                return Result<SignInCommandResponse>.Failure(AuthErrors.InvalidCredentials());

            if (!await _userService.IsEmailConfirmedAsync(user.Email))
                return Result<SignInCommandResponse>.Failure(AuthErrors.EmailConfirmationRequired());

            SignInCommandResponse? response = await _authService.SignInAsync(command.username, command.password);

            return response != null 
                ? Result<SignInCommandResponse>.Success(response)
                : Result<SignInCommandResponse>.Failure(AuthErrors.InvalidCredentials());
        }
    }
}
