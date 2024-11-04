using Application.Abstractions;
using Domain.DomainUsers;
using MediatR;

namespace Application.UseCases.DomainUsers.Queries.GetGardenerById
{
    internal class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<DomainUser>>
    {
        private readonly IDomainUserRepository _gardenerRepository;

        public GetUserByIdQueryHandler(IDomainUserRepository gardenerRepository)
        {
            _gardenerRepository = gardenerRepository;
        }

        public async Task<Result<DomainUser>> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
        {
            DomainUser? user = await _gardenerRepository.GetByIdAsync(query.id);

            if (user == null)
                return Result<DomainUser>.Failure(DomainUserErrors.NotFound(query.id));

            return Result<DomainUser>.Success(user);
        }
    }
}
