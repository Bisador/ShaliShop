namespace OrderModule.Domain.Products.DomainEvents;

public record ProductOutOfStock(Guid AggregateId) : DomainEvent(AggregateId);