namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemRemovedFromCart(Guid AggregateId, Guid ProductId) : DomainEvent(AggregateId);