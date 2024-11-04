using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.UpdatePlant
{
    public sealed record UpdatePlantCommand(PlantId id, string commonName) : IRequest<Result<Plant>>;
}
