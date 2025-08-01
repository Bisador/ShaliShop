using Microsoft.EntityFrameworkCore;
using Shared.Application;
using Shared.Application.Events;
using Shared.Domain;

namespace Shared.Persistence;

public abstract class UnitOfWorkBase(DbContext dbContext, DomainEventDispatcher dispatcher) : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        await dbContext.SaveChangesAsync(cancellationToken);
        await DispatchDomainEventsAsync(cancellationToken);
    }

    public async Task DispatchDomainEventsAsync(CancellationToken cancellationToken = default)
    {
        var aggregates = dbContext.ChangeTracker
            .Entries<AggregateRoot>()
            .Where(e => e.Entity.DomainEvents.Count != 0)
            .Select(e => e.Entity)
            .ToList();

        foreach (var aggregate in aggregates)
        {
            await dispatcher.DispatchAsync(aggregate, cancellationToken);
        }
    }
}