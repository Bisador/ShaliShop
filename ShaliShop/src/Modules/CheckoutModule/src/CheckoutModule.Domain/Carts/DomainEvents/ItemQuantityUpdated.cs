namespace CheckoutModule.Domain.Carts.DomainEvents;

public record ItemQuantityUpdated(Guid AggregateId, Guid ProductId, decimal NewQuantity) : DomainEvent(AggregateId);