using Domain.DomainUsers;

namespace Application.UseCases.DomainUsers
{
    public interface IDomainUserRepository
    {
        Task<List<DomainUser>> GetAsync();
        Task<DomainUser?> GetByIdAsync(DomainUserId id);
        void Add(DomainUser gardener);
        void Remove(DomainUser gardener);
    }
}
