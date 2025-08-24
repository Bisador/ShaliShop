using Shared.Domain;
using Shared.Eventing.Abstraction;
using Shared.Messaging.Abstraction;

namespace Shared.Messaging;

public class MessageBusDomainEventPublisher(IMessagePublisher messagePublisher) : IDomainEventPublisher
{
    public Task PublishAsync(IDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        var topic = domainEvent.GetType().Name;
        return messagePublisher.PublishAsync(domainEvent, topic, cancellationToken);
    }
}