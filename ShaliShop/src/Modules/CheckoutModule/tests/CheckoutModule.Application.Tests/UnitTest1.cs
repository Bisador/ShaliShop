using CheckoutModule.Application.Abstraction;
using CheckoutModule.Application.Carts.Commands.Checkout;
using CheckoutModule.Application.Models;
using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.Repository;
using InventoryModule.Domain.Inventories.Repository;
using OrderModule.Application.Abstraction;
using OrderModule.Domain.Orders.Aggregates;
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Repository;
using OrderModule.Domain.Orders.ValueObjects;
using Shared.Application.Messaging;
using Shared.Common;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Tests;

public class CartCheckoutCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCheckoutSuccessfully_WhenCartAndInventoryValid()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var customerId = Guid.NewGuid();
        var shippingDto = new ShippingAddressDto("Tehran", "Azadi St.", "11111", "Tehran", "Iran");
        var command = new CartCheckoutCommand(cartId, shippingDto);

        var cart = Cart.Create(customerId);
        cart.AddItem(Guid.NewGuid(), "Test Product", Money.From(2), 100);

        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(cartId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        var inventory = new Mock<IInventoryRepository>();
        inventory.Setup(i => i.TryReserveStockAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var orderItems = cart.Items.Select(p => new OrderItem(p.ProductId, p.ProductName, p.Quantity, p.UnitPrice)).ToList();
        var order = Order.Place(customerId, orderItems, new ShippingAddress(shippingDto.City, shippingDto.Street, shippingDto.ZipCode, shippingDto.State, shippingDto.Country));

        var orderFactory = new Mock<IOrderFactory>();
        orderFactory.Setup(f => f.CreateFromCart(cart, customerId, It.IsAny<ShippingAddress>()))
            .Returns(order);

        var orderRepo = new Mock<IOrderRepository>();
        var eventPublisher = new Mock<IIntegrationEventPublisher>();

        var checkoutUow = new Mock<ICheckoutUnitOfWork>();
        var orderUow = new Mock<IOrderUnitOfWork>();

        var handler = new CartCheckoutCommandHandler(
            cartRepo.Object, inventory.Object, orderFactory.Object, orderRepo.Object,
            eventPublisher.Object, checkoutUow.Object, orderUow.Object
        );

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.OrderId.Should().Be(order.Id);
        result.Value.TotalAmount.Should().Be(order.TotalAmount);

        orderRepo.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        cartRepo.Verify(r => r.SaveAsync(It.Is<Cart>(c => c.Items.Count == 0), It.IsAny<CancellationToken>()), Times.Once);
        eventPublisher.Verify(p => p.PublishAsync(It.IsAny<OrderPlaced>(), It.IsAny<CancellationToken>()), Times.Once);
        checkoutUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        orderUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}