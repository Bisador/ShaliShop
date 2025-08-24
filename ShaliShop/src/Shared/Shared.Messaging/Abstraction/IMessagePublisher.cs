namespace Shared.Messaging.Abstraction;

public interface IMessagePublisher
{
    Task PublishAsync<T>(T message, string topic,CancellationToken cancellationToken = default);
}