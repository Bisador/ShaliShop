namespace OrderService.Domain.Products.DomainEvents;

public record ProductUpdated(Guid AggregateId) : DomainEvent(AggregateId);