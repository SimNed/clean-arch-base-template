using Application.UseCases.Auth.Queries.ConfirmEmail;
using Application.UseCases.Auth.Commands.SignIn;
using Application.UseCases.Auth.Commands.SignUp;

namespace Application.UseCases.Auth
{
    public interface IAuthService
    {
        Task<SignInCommandResponse?> SignInAsync(string email, string password);
        Task<SignUpCommandResponse?> SignUpAsync(string username, string email, string password);
        Task SignOutAsync();
        Task<ConfirmEmailQueryResponse?> ConfirmEmailAsync(string email, string code);
        Task SendConfirmationEmailAsync(string email);
        Task SendPasswordResetCodeAsync(string email);
    }
}
