using OrderModule.Domain.Orders.Enums;

namespace OrderModule.Domain.Orders.Exceptions;

public class OnlyPaidOrdersCanBeConfirmedException(OrderStatus status) : BusinessRuleValidationException("Only paid orders can be confirmed.")
{
    public override bool IsBroken() =>status != OrderStatus.Paid;
}