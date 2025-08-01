using OrderModule.Domain.Orders.Enums;

namespace OrderModule.Domain.Orders.Exceptions;

public class OnlyPlacedOrdersCanBeCancelledException(OrderStatus status) : DomainException("Only placed orders can be Cancelled.")
{
    public override bool IsBroken() =>status != OrderStatus.Placed;
}