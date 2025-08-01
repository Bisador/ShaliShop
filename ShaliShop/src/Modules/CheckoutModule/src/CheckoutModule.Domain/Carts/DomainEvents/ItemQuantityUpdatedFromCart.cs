namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemQuantityUpdatedFromCart(Guid AggregateId, Guid ProductId, decimal NewQuantity, decimal OldQuantity) : DomainEvent(AggregateId);