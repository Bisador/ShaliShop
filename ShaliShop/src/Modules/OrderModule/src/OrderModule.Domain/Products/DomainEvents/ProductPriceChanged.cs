namespace OrderModule.Domain.Products.DomainEvents;

public record ProductPriceChanged(Guid AggregateId, Money NewPrice) : DomainEvent(AggregateId);