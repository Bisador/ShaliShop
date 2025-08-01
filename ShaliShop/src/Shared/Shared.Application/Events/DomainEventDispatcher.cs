using Shared.Domain;

namespace Shared.Application.Events;

public class DomainEventDispatcher(IDomainEventPublisher publisher)
{
    public async Task DispatchAsync(AggregateRoot aggregate, CancellationToken cancellationToken = default)
    {
        foreach (var domainEvent in aggregate.DomainEvents)
        {
            await publisher.PublishAsync(domainEvent, cancellationToken);
        }

        aggregate.ClearDomainEvents();
    }
}