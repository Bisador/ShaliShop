using OrderModule.Domain.Orders.Enums;

namespace OrderModule.Domain.Orders.Exceptions;

public class OnlyPaidOrdersCanBeShippedException(OrderStatus status) : BusinessRuleValidationException("Only paid orders can be shipped.")
{
    public override bool IsBroken() =>status != OrderStatus.Paid;
}