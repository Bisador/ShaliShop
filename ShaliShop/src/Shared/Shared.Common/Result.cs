using System.Collections.Immutable; 

namespace Shared.Common;

public class Result
{
    public bool IsSuccess { get; }
    public ImmutableList<Error>? Errors { get; }

    public static Result Success() => new();

    private Result(ImmutableList<Error> errors) => Errors = errors;
    private Result() => IsSuccess = true;

    public static Result Failure(List<Error> error) => new(ImmutableList.Create(error.ToArray()));
    public static Result Failure(Error error) => new([error]);
    public static Result Failure(string errorCode, string errorMessage) => Failure(new Error(errorCode, errorMessage));
    public static Result Failure(string errorMessage) => Failure(new Error(string.Empty, errorMessage));
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public ImmutableList<Error>? Errors { get; }

    private Result(ImmutableList<Error> errors) => Errors = errors;
    private Result(T? value) => (Value, IsSuccess) = (value, true);

    public static Result<T> Success(T value) => new(value);

    public static Result<T> Failure(Error error) => new([error]);
    public static Result<T> Failure(List<Error> error) => new(ImmutableList.Create(error.ToArray()));
    public static Result<T> Failure(string errorCode, string errorMessage) => Failure(new Error(errorCode, errorMessage));
    public static Result<T> Failure(string errorMessage) => Failure(new Error(string.Empty, errorMessage));
}

public record Error(string Code, string Message);