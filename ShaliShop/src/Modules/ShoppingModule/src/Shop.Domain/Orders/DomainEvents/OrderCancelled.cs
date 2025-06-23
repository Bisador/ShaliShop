namespace Shop.Domain.Orders.DomainEvents;

public record OrderCancelled(Guid OrderId, string Reason) : DomainEvent;