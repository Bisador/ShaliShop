namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderCancelled(Guid OrderId, string Reason) : DomainEvent;