namespace Shared.Application;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken = default);
    Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default);
    
}
 