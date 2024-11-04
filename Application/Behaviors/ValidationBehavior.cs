using Application.Abstractions;
using Application.Abstractions.Errors;
using FluentValidation;
using MediatR;

namespace Application.Behaviors
{ 
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IAppResult
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                List<string> errorMessages = failures.Select(f => f.ErrorMessage).ToList();

                ValidationError validationError = ValidationError.BadRequest(errorMessages);

                var responseType = typeof(TResponse);

                var resultType = typeof(IAppResult).IsAssignableFrom(responseType) && responseType.IsGenericType
                    ? typeof(Result<>).MakeGenericType(responseType.GetGenericArguments()[0])
                    : typeof(Result);

                return (TResponse)resultType.GetMethod("Failure")!
                        .Invoke(null, new object[] { validationError })!;
            }

            return await next();
        }
    }
}