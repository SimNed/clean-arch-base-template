using Application.Abstractions;
using MediatR;

namespace Application.UseCases.Auth.Queries.ConfirmEmail
{
    public sealed record ConfirmEmailQuery(Guid id, string code) : IRequest<Result<ConfirmEmailQueryResponse>>;
}
