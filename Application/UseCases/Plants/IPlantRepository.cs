using Domain.Plants;

namespace Application.UseCases.Plants
{
    public interface IPlantRepository
    {
        Task<List<Plant>> GetAsync();
        Task<Plant?> GetByIdAsync(PlantId id);
        Task<Plant?> GetByCommonNameAsync(string commonName);
        void Add(Plant plant);
        void Update(Plant plant);
        void Remove(Plant plant);
    }
}