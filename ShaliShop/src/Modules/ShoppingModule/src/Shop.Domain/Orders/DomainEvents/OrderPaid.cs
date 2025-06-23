namespace Shop.Domain.Orders.DomainEvents;

public record OrderPaid(Guid OrderId, string TransactionId) : DomainEvent;