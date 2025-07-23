using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.DomainEvents;

public record OrderReturned(Guid OrderId, IReadOnlyCollection<OrderItem> ReturnedItems) : DomainEvent;