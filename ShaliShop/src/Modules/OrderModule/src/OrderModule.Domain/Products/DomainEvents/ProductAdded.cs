namespace OrderModule.Domain.Products.DomainEvents;

public record ProductAdded(Guid AggregateId, string Name, Money Price) : DomainEvent(AggregateId);