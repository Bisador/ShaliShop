using Shop.Domain.Orders.ValueObjects;

namespace Shop.Domain.Orders.Rules;

public class OrderMustHaveAtLeastOneItem(IEnumerable<OrderItem> items) : IBusinessRule
{
    public bool IsBroken() => !items.Any();
    public string Message => "Order must contain at least one item.";
}

 