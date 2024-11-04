using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.DeletePlant
{
    internal class DeletePlantCommandHandler : IRequestHandler<DeletePlantCommand, Result<Plant>>
    {
        private readonly IPlantRepository _plantRepository;

        public DeletePlantCommandHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Result<Plant>> Handle(DeletePlantCommand command, CancellationToken cancellationToken)
        {
            var plant = await _plantRepository.GetByIdAsync(command.id);

            if (plant == null)
                return Result<Plant>.Failure(PlantErrors.NotFound());

            _plantRepository.Remove(plant);

            return Result<Plant>.Success(plant);
        }
    }
}
