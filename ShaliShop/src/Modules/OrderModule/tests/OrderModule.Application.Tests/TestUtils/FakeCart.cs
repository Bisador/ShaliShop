using CheckoutModule.Domain.Carts.Aggregates;
using SharedModule.Domain.ValueObjects;

namespace OrderModule.Application.Tests.TestUtils;

public static class FakeCart
{
    public static Cart WithItem(Guid customerId, string productName, Guid productId, int quantity, Money? money = null)
    {
        var cart = Cart.Create(customerId);

        cart.AddItem(
            productId: productId,
            productName: productName,
            unitPrice: money ?? Money.From(25.0m),
            quantity: quantity
        );

        return cart;
    }

    public static Cart Empty(Guid? customerId = null) => Cart.Create(customerId ?? Guid.NewGuid());
}