namespace OrderService.Domain.Customers.DomainEvents;

public record CustomerDeactivated(Guid AggregateId) : DomainEvent(AggregateId);