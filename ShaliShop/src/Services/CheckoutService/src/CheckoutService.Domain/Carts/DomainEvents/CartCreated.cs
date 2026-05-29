namespace CheckoutService.Domain.Carts.DomainEvents;

public record CartCreated(Guid AggregateId, Guid CustomerId) : DomainEvent(AggregateId);