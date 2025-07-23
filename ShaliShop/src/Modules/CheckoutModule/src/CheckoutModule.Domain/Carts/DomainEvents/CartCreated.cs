namespace CheckoutModule.Domain.Carts.DomainEvents;

public record CartCreated(Guid CartId, Guid CustomerId) : DomainEvent;