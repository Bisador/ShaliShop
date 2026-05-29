using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Domain.Orders.DomainEvents;

public record OrderReturned(Guid AggregateId, IReadOnlyCollection<OrderItem> ReturnedItems) : DomainEvent(AggregateId);