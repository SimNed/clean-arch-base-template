using Application.Abstractions.Errors;
using Domain.DomainUsers;

namespace Application.UseCases.DomainUsers
{
    public static class DomainUserErrors
    {
        public static Error NotFound() => Error.NotFound("Gardeners.NotFound", $"Gardeners not found");
        public static Error NotFound(DomainUserId id) => Error.NotFound("Gardeners.NotFound", $"Gardener with {id} not found");
        public static Error NotFound(string commonName) => Error.NotFound("Gardeners.NotFound", $"Gardener named '{commonName}' not found");
        public static Error AlreadyExist(DomainUserId id) => Error.Conflict("Gardeners.NotUnique", $"A plant named '{id}' already exist");
    }
}
