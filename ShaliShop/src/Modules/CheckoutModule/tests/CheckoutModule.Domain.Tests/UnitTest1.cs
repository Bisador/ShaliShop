using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.DomainEvents;
using CheckoutModule.Domain.Carts.Rules;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Domain.Tests;

public class CartTests
{
    [Fact]
    public void Create_ShouldSetCustomerId_AndEmitCartCreatedEvent()
    {
        var customerId = Guid.NewGuid();
        var cart = Cart.Create(customerId);

        cart.CustomerId.Should().Be(customerId);
        cart.CreatedAt.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        cart.Events.Should().ContainSingle(e => e is CartCreated);
    }

    [Fact]
    public void AddItem_ShouldAddNewItem_AndEmitItemAddedEvent()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();
        var price = Money.From(99.99m);

        cart.AddItem(productId, "Mouse", price, 2);

        cart.Items.Should().ContainSingle(i => i.ProductId == productId);
        cart.Items.First().Quantity.Should().Be(2);
        cart.Events.Should().ContainSingle(e => e is ItemAddedToCart);
    }

    [Fact]
    public void AddItem_ShouldUpdateQuantity_WhenProductAlreadyExists()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();
        var price = Money.From(59);

        cart.AddItem(productId, "Keyboard", price, 1);
        cart.AddItem(productId, "Keyboard", price, 2); // Adds total 3

        cart.Items.Single().Quantity.Should().Be(3);
        cart.Events.Should().Contain(e => e is ItemQuantityUpdated);
    }

    [Fact]
    public void RemoveItem_ShouldRemove_WhenItemExists()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "SSD", Money.From(120), 1);
        cart.RemoveItem(productId);

        cart.Items.Should().BeEmpty();
        cart.Events.Should().Contain(e => e is ItemRemovedFromCart);
    }

    [Fact]
    public void RemoveItem_ShouldThrow_WhenProductNotFound()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var invalidProduct = Guid.NewGuid();

        var act = () => cart.RemoveItem(invalidProduct);

        act.Should().Throw<ProductNotFoundException>();
    }

    [Fact]
    public void UpdateItemQuantity_ShouldModifyValue_AndEmitEvent()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "Router", Money.From(150), 1);
        cart.UpdateItemQuantity(productId, 5);

        cart.Items.Single().Quantity.Should().Be(5);
        cart.Events.Should().Contain(e => e is ItemQuantityUpdatedFromCart);
    }

    [Fact]
    public void Clear_ShouldRemoveAllItems_AndEmitCartCleared()
    {
        var cart = Cart.Create(Guid.NewGuid());
        cart.AddItem(Guid.NewGuid(), "Book", Money.From(10), 1);
        cart.AddItem(Guid.NewGuid(), "Pen", Money.From(2), 3);

        cart.Clear();

        cart.Items.Should().BeEmpty();
        cart.Events.Should().Contain(e => e is CartCleared);
    }
}