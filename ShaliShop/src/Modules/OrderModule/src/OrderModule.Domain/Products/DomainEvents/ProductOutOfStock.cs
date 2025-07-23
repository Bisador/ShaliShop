namespace OrderModule.Domain.Products.DomainEvents;

public record ProductOutOfStock(Guid ProductId) : DomainEvent;