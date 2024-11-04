using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Queries.GetPlantById
{
    internal class GetPlantByIdQueryHandler : IRequestHandler<GetPlantByIdQuery, Result<Plant>>
    {
        private readonly IPlantRepository _plantRepository;

        public GetPlantByIdQueryHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Result<Plant>> Handle(GetPlantByIdQuery query, CancellationToken cancellationToken)
        {
            Plant? plant = await _plantRepository.GetByIdAsync(query.id);

            if (plant == null)
                return Result<Plant>.Failure(PlantErrors.NotFound());

            return Result<Plant>.Success(plant);
        }
    }
}
