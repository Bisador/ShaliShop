using OrderService.Domain.Orders.Enums;

namespace OrderService.Domain.Orders.Exceptions;

public class OnlyPaidOrdersCanBeShippedException(OrderStatus status) : DomainException("Only paid orders can be shipped.")
{
    public override bool IsBroken() =>status != OrderStatus.Paid;
}