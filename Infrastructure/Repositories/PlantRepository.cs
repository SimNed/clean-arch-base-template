using Application.UseCases.Plants;
using Domain.Plants;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class PlantRepository : IPlantRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        
        public async Task<List<Plant>> GetAsync()
        {
            return await _context.Plant.ToListAsync();
        }
        
        public Task<Plant?> GetByIdAsync(PlantId id)
        {
            return _context.Plant.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Plant?> GetByCommonNameAsync(string commonName)
        {
            return await _context.Plant.SingleOrDefaultAsync(p => p.CommonName == commonName);
        }

        public void Add(Plant plant)
        {
            _context.Plant.Add(plant);
            _context.SaveChangesAsync();
        }

        public void Update(Plant plant)
        {
            _context.Plant.Update(plant);
            _context.SaveChangesAsync();
        }

        public void Remove(Plant plant)
        {
            _context.Plant.Remove(plant);
            _context.SaveChangesAsync();
        }
    }
}
