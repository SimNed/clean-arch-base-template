using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.UpdatePlant
{
    internal class UpdatePlantCommandHandler : IRequestHandler<UpdatePlantCommand, Result<Plant>>
    {
        private readonly IPlantRepository _plantRepository;

        public UpdatePlantCommandHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Result<Plant>> Handle(UpdatePlantCommand command, CancellationToken cancellationToken)
        {
            var plant = await _plantRepository.GetByIdAsync(command.id);

            if (plant == null)
                return Result<Plant>.Failure(PlantErrors.NotFound());

            plant.Update(commonName: command.commonName);

            _plantRepository.Update(plant);

            return Result<Plant>.Success(plant);
        }
    }
}
