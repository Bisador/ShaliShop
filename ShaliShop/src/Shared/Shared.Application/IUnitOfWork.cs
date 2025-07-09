namespace Shared.Application;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
}