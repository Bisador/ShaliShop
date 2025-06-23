using Shop.Domain.Orders.ValueObjects;

namespace Shop.Domain.Orders.DomainEvents;

public record OrderReturned(Guid OrderId, IReadOnlyCollection<OrderItem> ReturnedItems) : DomainEvent;