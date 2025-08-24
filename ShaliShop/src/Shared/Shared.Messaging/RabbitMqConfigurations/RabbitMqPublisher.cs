using System.Text;
using System.Text.Json; 
using RabbitMQ.Client;
using Shared.Messaging.Abstraction;

namespace Shared.Messaging.RabbitMqConfigurations;

public class RabbitMqPublisher : IMessagePublisher, IAsyncDisposable
{
    private readonly IConnection _connection;
    // private readonly ILogger<RabbitMqPublisher> _logger;
    // private readonly AsyncPolicy _retryPolicy;

    public static async Task<RabbitMqPublisher> CreateAsync(string hostName)
    {
        var factory = new ConnectionFactory {HostName = hostName};
        var connection = await factory.CreateConnectionAsync();

        return new RabbitMqPublisher(connection);
    }

    private RabbitMqPublisher(IConnection connection)
    {
        _connection = connection;
        // _logger = logger;
        // _retryPolicy = retryPolicy;
        //
        // _retryPolicy = Policy
        //     .Handle<Exception>()
        //     .WaitAndRetryAsync(3, i => TimeSpan.FromSeconds(i),
        //         (ex, timeSpan, retryCount, context) => { _logger.LogWarning(ex, "Retry {RetryCount} after {Delay}s", retryCount, timeSpan.TotalSeconds); });
    }

    public async Task PublishAsync<T>(T message, string topic, CancellationToken cancellationToken = default)
    {
        var channel = await _connection.CreateChannelAsync(cancellationToken: cancellationToken);

        var properties = new BasicProperties
        {
            Persistent = true,
            ContentType = "application/json",
            Type = typeof(T).Name
        };

        await channel.ExchangeDeclareAsync(topic, ExchangeType.Fanout, cancellationToken: cancellationToken);

        var envelope = new MessageEnvelope<T>(message);
        var json = JsonSerializer.Serialize(envelope);
        var body = Encoding.UTF8.GetBytes(json);

        await channel.BasicPublishAsync(exchange: topic, routingKey: "", mandatory: false, basicProperties: properties, body: body, cancellationToken: cancellationToken);
        
        //_logger.LogInformation("📤 Published {MessageType} to {Topic}", typeof(T).Name, topic);
    }

    public async ValueTask DisposeAsync()
    {
        await _connection.DisposeAsync();
    }
}