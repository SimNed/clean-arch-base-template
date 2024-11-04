using Application.Abstractions;
using Application.Abstractions.Errors;

namespace WebApi.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProblemDetails<TResult>(this TResult result) where TResult : IAppResult
        {
            if (result.Error == null)
            {
                throw new InvalidOperationException("Can't convert success result to problem");
            }

            Dictionary<string, object?> extensions = new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error.Code, result.Error.Description } }
            };

            if (result.Error is ValidationError validationError)
            {
                extensions.Add("validationErrors", validationError.ValidationDescriptions);
            }

            return Results.Problem(
                statusCode: GetStatusCode(result.Error.Type),
                title: GetTitle(result.Error.Type),
                type: GetType(result.Error.Type),
                extensions: extensions
            );
        }

        private static int GetStatusCode(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status500InternalServerError
            };

        private static string GetTitle(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "Bad Request",
                ErrorType.NotFound => "Not Found",
                ErrorType.Conflict => "Conflict",
                _ => "Server Error"
            };

        private static string GetType(ErrorType errorType) =>
            errorType switch
            {
                ErrorType.Validation => "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                ErrorType.NotFound => "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                ErrorType.Conflict => "https://tools.ietf.org/html/rfc7231#section-6.5.8",
                _ => "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };
    }
}
