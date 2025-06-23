namespace Shop.Domain.Carts.DomainEvents;

public record CartCleared(Guid CartId) : DomainEvent;