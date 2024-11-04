using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Queries.GetPlantById
{
    public sealed record GetPlantByIdQuery(PlantId id) : IRequest<Result<Plant>>;
}
