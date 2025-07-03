using Shared.Common;
using Shared.Domain;

namespace Shared.Application.Behavior;

public class DomainExceptionPipelineBehavior<TCommand, TResponse>
    : IPipelineBehavior<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
{
    public Task<Result<TResponse>> Handle(TCommand request, RequestHandlerDelegate<Result<TResponse>> next, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
//
// public class DomainExceptionPipelineBehavior<TCommand, TResponse> 
//     : IPipelineBehavior<TCommand, Result<TResponse>>
//     where TCommand : ICommand<TResponse>
// {
//     public async Task<Result<TResponse>> Handle(
//         TCommand request,
//         RequestHandlerDelegate<Result<TResponse>> next,
//         CancellationToken ct)
//     {
//         try
//         {
//             return await next();
//         }
//         catch (BusinessRuleValidationException ex)
//         {
//             return Result<TResponse>.Failure("DOMAIN_RULE_VIOLATION", ex.Message);
//         } 
//     }
// }