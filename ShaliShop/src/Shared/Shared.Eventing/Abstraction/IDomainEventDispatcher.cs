using Shared.Domain;

namespace Shared.Eventing.Abstraction;

public interface IDomainEventDispatcher
{
    Task DispatchAsync(AggregateRoot aggregate, CancellationToken cancellationToken = default);
    Task DispatchAsync(List<AggregateRoot> aggregates, CancellationToken cancellationToken = default);
}