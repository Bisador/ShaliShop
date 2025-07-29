using OrderModule.Domain.Orders.Enums;

namespace OrderModule.Domain.Orders.Exceptions;

public class OnlyShippedOrdersCanBeReturnedException(OrderStatus status) : BusinessRuleValidationException("Only shipped orders can be returned.")
{
    public override bool IsBroken() =>status != OrderStatus.Shipped;
}