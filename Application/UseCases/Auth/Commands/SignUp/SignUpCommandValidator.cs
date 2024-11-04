using FluentValidation;

namespace Application.UseCases.Auth.Commands.SignUp
{
    public class SignUpCommandValidator : AbstractValidator<SignUpCommand>
    {
        public SignUpCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(2, 32).WithMessage("Name must be between 6 and 32 characters.");

            RuleFor(x => x.email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(2, 50).WithMessage("Password must be at least 6 characters.")
                .Matches(@"(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d]).+")
                .WithMessage("Password must contain an uppercase character, lowercase character, a digit, and a non-alphanumeric character.");
        }
    }
}
