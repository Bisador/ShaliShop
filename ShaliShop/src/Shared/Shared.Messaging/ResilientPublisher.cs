using Polly; 
using Shared.Messaging.Abstraction;

namespace Shared.Messaging;

public class ResilientPublisher(IMessagePublisher inner) : IMessagePublisher
{
    public async Task PublishAsync<T>(T message, string topic, CancellationToken cancellationToken = default)
    {
        var policy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i));

        await policy.ExecuteAsync(() => inner.PublishAsync(message, topic, cancellationToken));
    }
}