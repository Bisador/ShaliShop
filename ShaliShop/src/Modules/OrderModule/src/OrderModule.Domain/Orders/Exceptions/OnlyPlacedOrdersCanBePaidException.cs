using OrderModule.Domain.Orders.Enums;

namespace OrderModule.Domain.Orders.Exceptions;

public class OnlyPlacedOrdersCanBePaidException(OrderStatus status) : DomainException("Only placed orders can be paid.")
{
    public override bool IsBroken() =>status != OrderStatus.Placed;
}