using Application.Abstractions.Errors;

namespace Application.UseCases.Plants
{
    public static class PlantErrors
    {
        public static Error NotFound() => Error.NotFound("Plants.NotFound", $"Plant not found");
        public static Error NotFound(string commonName) => Error.NotFound("Plants.NotFound", $"Plant named '{commonName}' not found");
        public static Error AlreadyExist(string commonName) => Error.Conflict("Plants.NotUnique", $"A plant named '{commonName}' already exist");
    }
}
