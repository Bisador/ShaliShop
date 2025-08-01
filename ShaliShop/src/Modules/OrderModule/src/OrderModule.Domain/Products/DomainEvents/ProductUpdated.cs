namespace OrderModule.Domain.Products.DomainEvents;

public record ProductUpdated(Guid AggregateId) : DomainEvent(AggregateId);