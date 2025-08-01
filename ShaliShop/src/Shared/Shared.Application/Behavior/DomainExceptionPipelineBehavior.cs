using System.Reflection;
using Shared.Common;
using Shared.Domain;

namespace Shared.Application.Behavior;

public class DomainExceptionPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next(cancellationToken);
        }
        catch (DomainException ex)
        {
            var responseType = typeof(TResponse);

            if (!responseType.IsGenericType ||
                responseType.GetGenericTypeDefinition() != typeof(Result<>)) throw; // Not a Result<T> — rethrow

            var error = new Error(DomainException.ErrorCode, ex.Message);
            var innerType = responseType.GetGenericArguments()[0];

            // Get the generic Result<T>
            var genericResultType = typeof(Result<>).MakeGenericType(innerType);

            // Get the static Failure method on Result<T>
            var failureMethod = genericResultType
                .GetMethods(BindingFlags.Public | BindingFlags.Static)
                .First(m => m.Name == "Failure" && m.GetParameters().Length == 1 && m.GetParameters()[0].ParameterType == typeof(Error));

            var failedResult = failureMethod.Invoke(null, [error]);

            return (TResponse) failedResult!;
        }
    }
}