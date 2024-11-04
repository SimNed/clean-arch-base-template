namespace Application.Abstractions.Errors
{
    public record ValidationError : Error
    {
        public List<string> ValidationDescriptions { get; private set; }

        public ValidationError(List<string> validationDescriptions)
            : base("Invalid.Credentials", "Invalid credentials", ErrorType.Validation)
        {
            ValidationDescriptions = validationDescriptions;
        }

        public static ValidationError BadRequest(List<string> validationDescriptions) => new(validationDescriptions);
    }
}
