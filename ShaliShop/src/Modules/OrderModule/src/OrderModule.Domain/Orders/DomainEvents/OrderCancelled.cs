namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderCancelled(Guid AggregateId, string Reason) : DomainEvent(AggregateId);