using Shop.Domain.Orders.ValueObjects;

namespace Shop.Domain.Orders.DomainEvents;

public record OrderPlaced(Guid OrderId, Guid CustomerId, IReadOnlyCollection<OrderItem> Items, Money TotalAmount)
    : DomainEvent;