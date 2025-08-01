using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderPlaced(Guid AggregateId, Guid CustomerId, IReadOnlyCollection<OrderItem> Items, Money TotalAmount)
    : DomainEvent(AggregateId);