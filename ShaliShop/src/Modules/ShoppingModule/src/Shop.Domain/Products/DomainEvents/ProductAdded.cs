namespace Shop.Domain.Products.DomainEvents;

public record ProductAdded(Guid ProductId, string Name, Money Price) : DomainEvent;