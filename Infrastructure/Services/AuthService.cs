using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Text;
using Application.UseCases.Auth;
using Application.UseCases.Auth.Commands.SignUp;
using Application.UseCases.Auth.Commands.SignIn;
using Application.UseCases.Auth.Queries.ConfirmEmail;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    internal sealed class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender<IdentityUser> _emailSender;
        private readonly TimeProvider _timeProvider;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private static readonly EmailAddressAttribute _emailAddressAttribute = new();

        public AuthService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IEmailSender<IdentityUser> emailSender, TimeProvider timeProvider, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _timeProvider = timeProvider;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<SignUpCommandResponse?> SignUpAsync(string username, string email, string password)
        {
            IdentityUser user = new IdentityUser { UserName = username, Email = email };

            IdentityResult registration = await _userManager.CreateAsync(user, password);

            return registration.Succeeded ? new SignUpCommandResponse(Guid.Parse(user.Id), user.UserName, user.Email) : null;
        }

        public async Task<SignInCommandResponse?> SignInAsync(string username, string password)
        {
            SignInResult result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: true);

            return result.Succeeded ? new SignInCommandResponse(username) : null;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<ConfirmEmailQueryResponse?> ConfirmEmailAsync(string email, string code)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);

            if (await _userManager.IsEmailConfirmedAsync(user))
                return null;

            code = DecodeEmailConfirmationCode(code);

            IdentityResult result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded ? new ConfirmEmailQueryResponse(email) : null;
        }

        public async Task SendConfirmationEmailAsync(string email)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);

            string? code = await GenerateEmailConfirmationCodeAsync(user);

            HttpContext context = _httpContextAccessor.HttpContext;

            string confirmationLink = $"{context.Request.Scheme}://{context.Request.Host}/auth/confirm-email?id={user.Id}&code={code}";
            await _emailSender.SendConfirmationLinkAsync(user, email, HtmlEncoder.Default.Encode(confirmationLink));
        }

        public async Task SendPasswordResetCodeAsync(string email)
        {
            IdentityUser? user = await _userManager.FindByEmailAsync(email);
            
            string? code = await GenerateForgotPasswordCodeAsync(user);

            await _emailSender.SendPasswordResetCodeAsync(user, email, HtmlEncoder.Default.Encode(code));
        }

        public async Task<string?> GenerateEmailConfirmationCodeAsync(IdentityUser user)
        {
            string? code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        }

        public string? DecodeEmailConfirmationCode(string code)
        {
            byte[]? decodedBytes = WebEncoders.Base64UrlDecode(code);

            return Encoding.UTF8.GetString(decodedBytes);
        }
        public async Task<string?> GenerateForgotPasswordCodeAsync(IdentityUser user)
        {
            string? code = await _userManager.GeneratePasswordResetTokenAsync(user);
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        }

    }
}