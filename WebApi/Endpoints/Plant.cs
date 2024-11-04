using Application.UseCases.Plants.Commands.CreatePlant;
using Application.UseCases.Plants.Commands.DeletePlant;
using Application.UseCases.Plants.Commands.UpdatePlant;
using Application.UseCases.Plants.Queries.GetPlantById;
using Application.UseCases.Plants.Queries.GetPlants;
using Domain.Plants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Endpoints
{
    public static class Plant
    {
        public static void AddPlantRoutes(this IEndpointRouteBuilder app)
        {
            string groupUrl = "/api/plants";

            RouteGroupBuilder group = app.MapGroup(groupUrl);

            group.MapGet("/", async ([FromServices]  ISender sender) =>
            {
                var result = await sender.Send(new GetPlantsQuery());

                return result.Map(
                    onSuccess: plants => Results.Ok(plants),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapGet("/{id:Guid}", async ([FromServices]  ISender sender, Guid id)=>
            {
                var result = await sender.Send(new GetPlantByIdQuery(new PlantId(id)));

                return result.Map(
                    onSuccess: plant => Results.Ok(plant),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapPost("/", async ([FromBody] CreatePlantCommand command, [FromServices] ISender sender, HttpContext httpContext) =>
            {
                var result = await sender.Send(command);

                return result.Map(
                    onSuccess: plant => Results.Created($"{httpContext.Request.Scheme}://{httpContext.Request.Host}{groupUrl}/{plant.Id.value}", null),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapPut("/", async ([FromBody] UpdatePlantCommand command, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.Map(
                    onSuccess: plant => Results.Ok(plant),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapDelete("/", async ([FromBody] DeletePlantCommand command, [FromServices] ISender sender) =>
            {
                var result = await sender.Send(command);

                return result.Map(
                    onSuccess: plant => Results.NoContent(),
                    onFailure: _ => result.ToProblemDetails()
                );
            });
        }
    }
}
