using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Domain.Orders.DomainEvents;

public record OrderPlaced(Guid AggregateId, Guid CustomerId, IReadOnlyCollection<OrderItem> Items, Money TotalAmount)
    : DomainEvent(AggregateId);