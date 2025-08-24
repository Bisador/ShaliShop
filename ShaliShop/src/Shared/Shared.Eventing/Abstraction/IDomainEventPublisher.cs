using Shared.Domain;

namespace Shared.Eventing.Abstraction;

public interface IDomainEventPublisher
{
    Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default);
}
