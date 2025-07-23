namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderConfirmed(Guid OrderId) : DomainEvent;