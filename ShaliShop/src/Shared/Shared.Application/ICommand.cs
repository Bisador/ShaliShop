using Shared.Common;

namespace Shared.Application;

public interface ICommand : IRequest<Result>;

public interface ICommand<T> : IRequest<Result<T>>;