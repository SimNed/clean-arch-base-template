using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.DeletePlant
{
    public sealed record DeletePlantCommand(PlantId id) : IRequest<Result<Plant>>;
}
