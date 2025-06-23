namespace Shop.Domain.Products.DomainEvents;

public record ProductPriceChanged(Guid ProductId, Money NewPrice) : DomainEvent;