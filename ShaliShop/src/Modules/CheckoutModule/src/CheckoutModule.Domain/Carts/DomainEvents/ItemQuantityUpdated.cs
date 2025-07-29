namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemQuantityUpdated(Guid CartId, Guid ProductId, decimal NewQuantity) : DomainEvent;