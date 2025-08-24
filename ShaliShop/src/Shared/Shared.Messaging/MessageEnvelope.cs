namespace Shared.Messaging;
 
public class MessageEnvelope<T>(T payload)
{
    public string MessageId { get; set; } = Guid.NewGuid().ToString();
    public string MessageType { get; set; } = typeof(T).Name;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public T Payload { get; set; } = payload;
}
