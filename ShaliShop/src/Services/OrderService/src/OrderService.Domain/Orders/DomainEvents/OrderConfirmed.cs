namespace OrderService.Domain.Orders.DomainEvents;

public record OrderConfirmed(Guid AggregateId) : DomainEvent(AggregateId);