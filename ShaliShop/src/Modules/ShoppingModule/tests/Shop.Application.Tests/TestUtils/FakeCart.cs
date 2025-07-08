using SharedModule.Domain.ValueObjects;
using Shop.Domain.Carts.Aggregates;

namespace Shop.Application.Tests.TestUtils;

public static class FakeCart
{
    public static Cart WithItem(string productName, Guid productId, int quantity,Money? money=null)
    {
        var cart = Cart.Create(Guid.NewGuid());

        cart.AddItem(
            productId: productId,
            productName: productName,
            unitPrice: money?? Money.Create(25.0m),
            quantity: quantity
        );

        return cart;
    }

    public static Cart Empty(Guid? customerId = null) => Cart.Create(customerId ?? Guid.NewGuid());
}