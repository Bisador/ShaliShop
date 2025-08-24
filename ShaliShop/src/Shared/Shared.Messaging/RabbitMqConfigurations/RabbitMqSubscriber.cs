// using System.Text;
// using System.Text.Json;
//
// namespace Shared.Messaging.RabbitMqConfigurations;
//
// public class RabbitMqSubscriber : IMessageSubscriber
// {
//     private readonly IConnection _connection;
//
//     public RabbitMqSubscriber(string hostName)
//     {
//         var factory = new ConnectionFactory { HostName = hostName };
//         _connection = factory.CreateConnection();
//     }
//
//     public Task SubscribeAsync<T>(string topic, Func<T, Task> handler)
//     {
//         var channel = _connection.CreateModel();
//         channel.ExchangeDeclare(topic, ExchangeType.Fanout);
//         var queueName = channel.QueueDeclare().QueueName;
//         channel.QueueBind(queue: queueName, exchange: topic, routingKey: "");
//
//         var consumer = new EventingBasicConsumer(channel);
//         consumer.Received += async (model, ea) =>
//         {
//             var body = ea.Body.ToArray();
//             var json = Encoding.UTF8.GetString(body);
//             var message = JsonSerializer.Deserialize<T>(json);
//             await handler(message);
//         };
//
//         channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
//         return Task.CompletedTask;
//     }
// }