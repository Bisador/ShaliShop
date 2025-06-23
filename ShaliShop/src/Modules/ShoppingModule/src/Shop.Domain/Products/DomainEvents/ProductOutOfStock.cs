namespace Shop.Domain.Products.DomainEvents;

public record ProductOutOfStock(Guid ProductId) : DomainEvent;