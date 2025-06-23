namespace Shop.Domain.Carts.DomainEvents;

public record ItemRemovedFromCart(Guid CartId, Guid ProductId) : DomainEvent;