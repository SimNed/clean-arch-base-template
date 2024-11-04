using Application.Abstractions;
using Application.UseCases.Auth.Commands.ForgotPassword;
using Application.UseCases.Auth.Commands.ResendConfirmationEmail;
using Application.UseCases.Auth.Commands.SignIn;
using Application.UseCases.Auth.Commands.SignOut;
using Application.UseCases.Auth.Commands.SignUp;
using Application.UseCases.Auth.Queries.ConfirmEmail;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Endpoints
{
    public static class Auth
    {
        public static void AddAuthRoutes(this IEndpointRouteBuilder app)
        {
            string groupUrl = "/auth";

            RouteGroupBuilder group = app.MapGroup(groupUrl);

            group.MapPost("/sign-up", async ([FromBody] SignUpCommand command, [FromServices] ISender sender, HttpContext httpContext) =>
            {
                Result<SignUpCommandResponse> result = await sender.Send(command);

                return result.Map(
                    onSuccess: user => Results.Created($"{httpContext.Request.Scheme}://{httpContext.Request.Host}{groupUrl}/{user.UserId}", null),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapPost("/sign-in", async ([FromBody] SignInCommand command, [FromServices] ISender sender) =>
            {
                Result<SignInCommandResponse> result = await sender.Send(command);

                return result.Map(
                    onSuccess: result => Results.Ok(result),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapPost("/sign-out", async ([FromBody] SignOutCommand command, [FromServices] ISender sender) =>
            {
                Result result = await sender.Send(command);

                return result.Map(
                    onSuccess: () => Results.Ok("Log out successfully !"),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapGet("/confirm-email", async ([FromQuery] string id, [FromQuery] string code, [FromServices] ISender sender) =>
            {
                Result<ConfirmEmailQueryResponse> result = await sender.Send(new ConfirmEmailQuery(Guid.Parse(id), code));

                return result.Map(
                    onSuccess: result => Results.Ok(result),
                    onFailure: _ => result.ToProblemDetails()
                );
            });

            group.MapPost("/resend-confirmation-email", async ([FromBody] ResendConfirmationEmailCommand command, [FromServices] ISender sender) =>
            {
                Result result = await sender.Send(command);

                return result.Map(
                    onSuccess: () => Results.Ok("Confirmation email resend !"),
                    onFailure: _ => result.ToProblemDetails()
                );
            });
            
            group.MapPost("/forgot-password", async ([FromBody] ForgotPasswordCommand command, [FromServices] ISender sender) =>
            {
                Result result = await sender.Send(command);

                return result.Map(
                    onSuccess: () => Results.Ok("Check your mailbox !"),
                    onFailure: _ => Results.BadRequest()
                );
            });
        }
    }
}
