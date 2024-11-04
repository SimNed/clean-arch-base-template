using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.CreatePlant
{
    internal sealed class CreatePlantCommandHandler : IRequestHandler<CreatePlantCommand, Result<Plant>>
    {
        private readonly IPlantRepository _plantRepository;

        public CreatePlantCommandHandler(IPlantRepository plantRepository)
        {
            _plantRepository = plantRepository;
        }

        public async Task<Result<Plant>> Handle(CreatePlantCommand command, CancellationToken cancellationToken)
        {
            Plant? existingPlant = await _plantRepository.GetByCommonNameAsync(command.plant.CommonName);

            if (existingPlant != null)
                return Result<Plant>.Failure(PlantErrors.AlreadyExist(command.plant.CommonName));

            Plant plant = new Plant(
                new PlantId(Guid.NewGuid()),
                command.plant.CommonName
            );

            _plantRepository.Add(plant);

            return Result<Plant>.Success(plant);
        }
    }
}
