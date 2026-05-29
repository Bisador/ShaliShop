using OrderService.Domain.Orders.Enums;

namespace OrderService.Domain.Orders.Exceptions;

public class OnlyPlacedOrdersCanBeCancelledException(OrderStatus status) : DomainException("Only placed orders can be Cancelled.")
{
    public override bool IsBroken() =>status != OrderStatus.Placed;
}