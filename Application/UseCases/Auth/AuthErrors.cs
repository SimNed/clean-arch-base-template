using Application.Abstractions.Errors;

namespace Application.UseCases.Auth
{
    public static class AuthErrors
    {
        public static Error RegistrationFailed() => Error.Failure("Auth.RegistrationFailed", $"Registration failed");
        public static Error InvalidCredentials() => Error.Failure("Auth.InavalidCredentials", $"Invalid credentials");
        public static Error ExistingUsername() => Error.Conflict("Auth.ExistingUsername", $"Username already exist");
        public static Error EmailNotFound() => Error.NotFound("Auth.EmailNotFound", $"No email related");
        public static Error EmailConfirmationRequired() => Error.Failure("Auth.EmailConfirmationRequired", $"Email must be confirmed for login");
        public static Error EmailConfirmationFailed() => Error.Failure("Auth.EmailConfirmationFailed", $"Email confirmation failed");
        public static Error EmailAlreadyConfirmed() => Error.Failure("Auth.EmailAlreadyConfirmed", $"Email already confirmed");
        public static Error SignOutFailed() => Error.Failure("Auth.SignOutFailed", "Sign out Failed");
    }
}
