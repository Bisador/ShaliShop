namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemRemovedFromCart(Guid CartId, Guid ProductId) : DomainEvent;