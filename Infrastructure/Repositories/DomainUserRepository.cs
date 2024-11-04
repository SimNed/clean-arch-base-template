using Application.UseCases.DomainUsers;
using Domain.DomainUsers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class DomainUserRepository : IDomainUserRepository
    {
        private readonly ApplicationDbContext _context;

        public DomainUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<List<DomainUser>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<DomainUser?> GetByIdAsync(DomainUserId id)
        {
            return _context.DomainUser.SingleOrDefaultAsync(u => u.UserId == id);
        }

        public void Add(DomainUser user)
        {
            _context.DomainUser.Add(user);
            _context.SaveChangesAsync();
        }

        public void Remove(DomainUser user)
        {
            _context.DomainUser.Remove(user);
            _context.SaveChangesAsync();
        }
    }
}
