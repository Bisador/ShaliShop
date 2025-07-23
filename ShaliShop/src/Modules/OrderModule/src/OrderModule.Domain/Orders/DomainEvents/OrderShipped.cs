namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderShipped(Guid OrderId) : DomainEvent;