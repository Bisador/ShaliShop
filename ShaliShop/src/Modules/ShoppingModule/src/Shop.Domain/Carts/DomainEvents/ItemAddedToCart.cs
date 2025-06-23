using Shop.Domain.Carts.ValueObjects;

namespace Shop.Domain.Carts.DomainEvents;

public record ItemAddedToCart(Guid CartId, CartItem Item): DomainEvent;