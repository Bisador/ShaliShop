using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.Exceptions;

public class OrderMustHaveAtLeastOneItemException(IEnumerable<OrderItem> items) : DomainException("Order must contain at least one item.")
{
    public override bool IsBroken() => !items.Any();
}