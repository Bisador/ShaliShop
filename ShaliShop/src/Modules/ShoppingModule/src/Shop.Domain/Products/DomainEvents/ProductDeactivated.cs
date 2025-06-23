namespace Shop.Domain.Products.DomainEvents;

public record ProductDeactivated(Guid ProductId) : DomainEvent;