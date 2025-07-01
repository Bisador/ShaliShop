using FluentValidation;
using Shared.Common;

namespace Shared.Application.Behavior
{
    public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct)
        {
            if (!validators.Any()) 
                return await next(); 

            var context = new ValidationContext<TRequest>(request); 
            var errors = validators
                .Select(x => x.Validate(context))
                .SelectMany(x => x.Errors)
                .Select(p => new Error(p.ErrorCode, p.ErrorMessage))
                .DistinctBy(p => p.Message).ToList();

            return errors.Count != 0 ? (TResponse) Result.Failure(errors) : await next();
        }
    }
}

