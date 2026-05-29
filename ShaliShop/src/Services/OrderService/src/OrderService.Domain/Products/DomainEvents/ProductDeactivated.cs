namespace OrderService.Domain.Products.DomainEvents;

public record ProductDeactivated(Guid AggregateId) : DomainEvent(AggregateId);