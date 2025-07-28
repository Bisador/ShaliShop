using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.ValueObjects;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Tests.TestUtils;

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

    public static Cart WithItem(Guid customerId)
    {
        var cart = Cart.Create(customerId);
        ;
        cart.AddItem(Guid.NewGuid(), "Test Product", Money.From(2), 100);
        return cart;
    }

    public static Cart Empty(Guid? customerId = null) => Cart.Create(customerId ?? Guid.NewGuid());
}

public class FakeCartBuilder(Guid customerId)
{
    private Guid _customerId = customerId;
    private readonly List<CartItem> _items = [];

    public FakeCartBuilder WithItem(Guid productId, int quantity, decimal unitPrice = 100, string name = "Test Product")
    {
        _items.Add(new CartItem(productId, name, quantity, Money.From(unitPrice)));
        return this;
    }

    public FakeCartBuilder WithDefaultItems(int count = 1)
    {
        for (var i = 0; i < count; i++)
        {
            _items.Add(new CartItem(Guid.NewGuid(), $"Product {i + 1}", 1, Money.From(50)));
        }
        return this;
    }

    public Cart Build()
    {
        var cart = Cart.Create(_customerId);
        foreach (var item in _items)
            cart.AddItem(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);
        return cart;
    }
}