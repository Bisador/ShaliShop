using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Domain.Orders.Rules;

public class OrderMustHaveAtLeastOneItem(IEnumerable<OrderItem> items) : IBusinessRule
{
    public bool IsBroken() => !items.Any();
    public string Message => "Order must contain at least one item.";
}

 