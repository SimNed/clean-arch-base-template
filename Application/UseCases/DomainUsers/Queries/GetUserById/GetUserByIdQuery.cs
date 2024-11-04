using Application.Abstractions;
using Domain.DomainUsers;
using MediatR;

namespace Application.UseCases.DomainUsers.Queries.GetGardenerById
{
    public sealed record GetUserByIdQuery(DomainUserId id) : IRequest<Result<DomainUser>>;
}
