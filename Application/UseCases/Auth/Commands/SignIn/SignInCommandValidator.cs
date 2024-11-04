using FluentValidation;

namespace Application.UseCases.Auth.Commands.SignIn
{
    public class SignInCommandValidator : AbstractValidator<SignInCommand>
    {
        public SignInCommandValidator()
        {
            RuleFor(x => x.username)
                .NotEmpty().WithMessage("Username is required.");

            RuleFor(x => x.password)
                .NotEmpty().WithMessage("Password is required.");
        }
    }
}

