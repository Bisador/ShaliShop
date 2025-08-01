namespace OrderModule.Domain.Products.DomainEvents;

public record ProductDeactivated(Guid AggregateId) : DomainEvent(AggregateId);