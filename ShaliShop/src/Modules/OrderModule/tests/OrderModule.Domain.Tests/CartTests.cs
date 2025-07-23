using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.DomainEvents;
using Shared.Domain;
using SharedModule.Domain.ValueObjects; 

namespace OrderModule.Domain.Tests;

public class CartTests
{
    [Fact]
    public void Can_add_a_single_item()
    {
        var cart = Cart.Create(Guid.NewGuid());

        cart.AddItem(productId: Guid.NewGuid(), productName: "Keyboard", unitPrice: Money.From(99.99m), quantity: 1);

        Assert.Single(cart.Items);
    }

    [Fact]
    public void Adding_same_product_should_increase_quantity()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "Mouse", Money.From(50m), 1);
        cart.AddItem(productId, "Mouse", Money.From(50m), 2);

        var item = cart.Items.First();
        Assert.Equal(3, item.Quantity);
    }

    [Fact]
    public void Cannot_add_item_with_zero_quantity()
    {
        var cart = Cart.Create(Guid.NewGuid());

        Assert.Throws<BusinessRuleValidationException>(() =>
            cart.AddItem(Guid.NewGuid(), "Invalid", Money.From(10m), 0));
    }

    [Fact]
    public void Can_remove_item()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "Camera", Money.From(250m), 1);
        cart.RemoveItem(productId);

        Assert.Empty(cart.Items);
    }

    [Fact]
    public void Can_clear_cart()
    {
        var cart = Cart.Create(Guid.NewGuid());

        cart.AddItem(Guid.NewGuid(), "Chair", Money.From(89m), 1);
        cart.AddItem(Guid.NewGuid(), "Desk", Money.From(199m), 1);
        cart.Clear();

        Assert.True(cart.IsEmpty);
    }

    [Fact]
    public void Adding_same_product_with_different_name_or_price_should_not_create_duplicate()
    {
        // Arrange
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        // Act
        cart.AddItem(productId, "Gaming Mouse", Money.From(49.99m), 1);
        cart.AddItem(productId, "Ergonomic Mouse", Money.From(59.99m), 1); // Different name and price

        // Assert
        cart.Items.Should().HaveCount(1);

        var item = cart.Items.First();
        item.ProductName.Should().Be("Gaming Mouse"); // Original
        item.UnitPrice.Amount.Should().Be(49.99m);
        item.Quantity.Should().Be(2); // Quantity increased
    }

    [Fact]
    public void Removing_nonexistent_item_should_throw()
    {
        var cart = Cart.Create(Guid.NewGuid());

        var nonexistentProductId = Guid.NewGuid();

        Action act = () => cart.RemoveItem(nonexistentProductId);

        act.Should().Throw<BusinessRuleValidationException>();
    }

    [Fact]
    public void Increasing_item_quantity_should_raise_ItemQuantityUpdated_event()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var productId = Guid.NewGuid();

        cart.AddItem(productId, "Bench", Money.From(299.99m), 1);
        cart.AddItem(productId, "Bench", Money.From(299.99m), 2);

        var domainEvents = cart.Events;

        domainEvents.Any(e =>
            e is ItemQuantityUpdated updated &&
            updated.ProductId == productId &&
            updated.NewQuantity == 3).Should().BeTrue();
    }

    [Fact]
    public void Clearing_cart_should_raise_CartCleared_event()
    {
        var cart = Cart.Create(Guid.NewGuid());

        cart.AddItem(Guid.NewGuid(), "Row Machine", Money.From(450m), 1);
        cart.Clear();

        var domainEvents = cart.Events;

        domainEvents.Should().ContainSingle(e => e is CartCleared);
    }

    [Fact]
    public void Modifying_cart_should_update_LastModified()
    {
        var cart = Cart.Create(Guid.NewGuid());
        var before = cart.LastModified;

        Thread.Sleep(10); // Ensure time tick

        cart.AddItem(Guid.NewGuid(), "Yoga Mat", Money.From(20m), 1);

        cart.LastModified.Should().BeAfter(before);
    }
}