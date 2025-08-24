namespace Shared.Eventing.Abstraction;

public interface IMessageSubscriber
{
    Task SubscribeAsync<T>(string topic, Func<T, Task> handler);
}