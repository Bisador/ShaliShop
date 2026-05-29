using OrderService.Domain.Orders.Enums;

namespace OrderService.Domain.Orders.Exceptions;

public class OnlyPaidOrdersCanBeConfirmedException(OrderStatus status) : DomainException("Only paid orders can be confirmed.")
{
    public override bool IsBroken() =>status != OrderStatus.Paid;
}