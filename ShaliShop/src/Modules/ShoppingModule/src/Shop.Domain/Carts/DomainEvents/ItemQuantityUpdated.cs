namespace Shop.Domain.Carts.DomainEvents;

public record ItemQuantityUpdated(Guid CartId, Guid ProductId, int NewQuantity) : DomainEvent;