namespace CheckoutModule.Domain.Carts.DomainEvents;

public record CartCleared(Guid AggregateId) : DomainEvent(AggregateId);