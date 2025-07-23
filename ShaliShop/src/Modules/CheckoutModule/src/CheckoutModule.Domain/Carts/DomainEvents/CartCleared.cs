namespace CheckoutModule.Domain.Carts.DomainEvents;

public record CartCleared(Guid CartId) : DomainEvent;