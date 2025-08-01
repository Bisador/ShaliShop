using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderReturned(Guid AggregateId, IReadOnlyCollection<OrderItem> ReturnedItems) : DomainEvent(AggregateId);