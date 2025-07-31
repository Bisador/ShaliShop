using Shared.Application.Queries;
using Shared.Common;

namespace Shared.Application;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;

public interface IQuery<TResponse> : IRequest<PaginatedResult<TResponse>>;