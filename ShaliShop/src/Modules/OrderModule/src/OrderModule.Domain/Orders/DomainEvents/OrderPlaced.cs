using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderPlaced(Guid OrderId, Guid CustomerId, IReadOnlyCollection<OrderItem> Items, Money TotalAmount)
    : DomainEvent;