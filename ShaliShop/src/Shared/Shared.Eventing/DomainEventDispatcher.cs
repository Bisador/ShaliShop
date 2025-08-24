using Shared.Domain;
using Shared.Eventing.Abstraction;

namespace Shared.Eventing;

public class DomainEventDispatcher(IDomainEventPublisher publisher) : IDomainEventDispatcher
{
    public async Task DispatchAsync(AggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in aggregate.DomainEvents)
        {
            await publisher.PublishAsync(domainEvent, cancellationToken);
        }

        aggregate.ClearDomainEvents();
    }

    public async Task DispatchAsync(List<AggregateRoot> aggregates, CancellationToken cancellationToken = default)
    { 
        foreach (var aggregate in aggregates)
        {
            await DispatchAsync(aggregate, cancellationToken);
        }
    }
}