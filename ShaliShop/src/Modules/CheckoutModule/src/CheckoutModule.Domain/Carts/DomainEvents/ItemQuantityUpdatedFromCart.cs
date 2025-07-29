namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemQuantityUpdatedFromCart(Guid CartId, Guid ProductId, decimal NewQuantity, decimal OldQuantity) : DomainEvent;