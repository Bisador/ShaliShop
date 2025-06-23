namespace Shop.Domain.Products.DomainEvents;

public record ProductUpdated(Guid ProductId) : DomainEvent;