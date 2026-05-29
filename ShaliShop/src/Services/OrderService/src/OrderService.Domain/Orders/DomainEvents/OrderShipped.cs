namespace OrderService.Domain.Orders.DomainEvents;

public record OrderShipped(Guid AggregateId) : DomainEvent(AggregateId);