using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Commands.CreatePlant
{
    public sealed record CreatePlantCommand(Plant plant) : IRequest<Result<Plant>>;
}