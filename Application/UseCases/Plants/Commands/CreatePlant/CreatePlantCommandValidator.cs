using FluentValidation;

namespace Application.UseCases.Plants.Commands.CreatePlant
{
    public class CreatePlantCommandValidator : AbstractValidator<CreatePlantCommand>
    {
        public CreatePlantCommandValidator()
        {
            RuleFor(x => x.plant.CommonName)
                    .NotEmpty().WithMessage("Username is required.")
                    .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.");
        }
    }
}
