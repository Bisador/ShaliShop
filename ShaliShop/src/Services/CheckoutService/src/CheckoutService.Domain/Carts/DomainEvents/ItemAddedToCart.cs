using CheckoutService.Domain.Carts.ValueObjects;

namespace CheckoutService.Domain.Carts.DomainEvents;

public record ItemAddedToCart(Guid AggregateId, CartItem Item): DomainEvent(AggregateId);