using CheckoutModule.Domain.Carts.ValueObjects;

namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemAddedToCart(Guid AggregateId, CartItem Item): DomainEvent(AggregateId);