using OrderService.Domain.Orders.Enums;

namespace OrderService.Domain.Orders.Exceptions;

public class OnlyShippedOrdersCanBeReturnedException(OrderStatus status) : DomainException("Only shipped orders can be returned.")
{
    public override bool IsBroken() =>status != OrderStatus.Shipped;
}