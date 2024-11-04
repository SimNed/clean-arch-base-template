using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Queries.GetPlants
{
    internal class GetPlantsQueryHandler : IRequestHandler<GetPlantsQuery, Result<List<Plant>>>
    {
        private readonly IPlantRepository _plantRepository;

        public GetPlantsQueryHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Result<List<Plant>>> Handle(GetPlantsQuery query, CancellationToken cancellationToken)
        {
            List<Plant> plants = await _plantRepository.GetAsync();

            if (plants == null)
                return Result<List<Plant>>.Failure(PlantErrors.NotFound());

            return Result<List<Plant>>.Success(plants);
        }
    }
}
