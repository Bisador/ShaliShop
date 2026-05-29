namespace OrderService.Domain.Orders.DomainEvents;

public record OrderPaid(Guid AggregateId, string TransactionId) : DomainEvent(AggregateId);