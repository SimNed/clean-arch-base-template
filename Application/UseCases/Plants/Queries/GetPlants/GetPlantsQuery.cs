using Application.Abstractions;
using Domain.Plants;
using MediatR;

namespace Application.UseCases.Plants.Queries.GetPlants
{
    public sealed record GetPlantsQuery() : IRequest<Result<List<Plant>>>;
}
