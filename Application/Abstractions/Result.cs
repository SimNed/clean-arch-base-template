using Application.Abstractions.Errors;

namespace Application.Abstractions
{
    public interface IAppResult
    {
        Error? Error { get; }
        bool IsSuccess { get; }
    }

    public class Result : IAppResult
    {
        private Result()
        {
            Error = null;
        }

        private Result(Error error)
        {
            Error = error;
        }

        public Error? Error { get; }
        public bool IsSuccess => Error == null;
        public static Result Success() => new Result();
        public static Result Failure(Error error) => new Result(error);
        public TResult Map<TResult>(Func<TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess() : onFailure(Error!);
        }
    }

    public class Result<TValue> : IAppResult
        where TValue : class
    {
        private Result(TValue value)
        {
            Value = value;
            Error = null;
        }

        private Result(Error error)
        {
            Error = error;
            Value = default;
        }

        public TValue? Value { get; }
        public Error? Error { get; }
        public bool IsSuccess => Error == null;

        public static Result<TValue> Success(TValue value) => new Result<TValue>(value);
        public static Result<TValue> Failure(Error error) => new Result<TValue>(error);
        public TResult Map<TResult>(Func<TValue, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value!) : onFailure(Error!);
        }
    }
}

