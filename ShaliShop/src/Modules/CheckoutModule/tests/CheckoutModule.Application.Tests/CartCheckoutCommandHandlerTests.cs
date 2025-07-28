using CheckoutModule.Application.Abstraction;
using CheckoutModule.Application.Carts.Commands.Checkout;
using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Application.Tests.TestUtils;
using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.Repository;
using InventoryModule.Domain.Inventories.Repository;
using OrderModule.Application.Abstraction;
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Repository;
using OrderModule.Domain.Orders.ValueObjects;
using Shared.Application.Messaging;
using Shared.Common;

namespace CheckoutModule.Application.Tests;

public class CartCheckoutCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCheckoutSuccessfully_WhenCartAndInventoryValid()
    {
        //  
        var customerId = Guid.NewGuid();
        var shippingAddress = FakeShippingAddressDto.FakeAddress();
        var cart = new FakeCartBuilder(customerId).WithDefaultItems().Build();
        var command = new CartCheckoutCommand(cart.Id, shippingAddress);

        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(cart.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        var inventory = new Mock<IInventoryRepository>();
        inventory.Setup(i => i.TryReserveStockAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var order = FakeOrder.FromCart(cart, shippingAddress);
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
        result.Value!.OrderId.Should().Be(order.Id);
        result.Value.TotalAmount.Should().Be(order.TotalAmount);

        orderRepo.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        cartRepo.Verify(r => r.SaveAsync(It.Is<Cart>(c => c.Items.Count == 0), It.IsAny<CancellationToken>()), Times.Once);
        eventPublisher.Verify(p => p.PublishAsync(It.IsAny<OrderPlaced>(), It.IsAny<CancellationToken>()), Times.Once);
        checkoutUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
        orderUow.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldFail_WhenCheckoutCommitFails()
    {
        // Arrange
        var shippingAddress = FakeShippingAddressDto.FakeAddress(); 
        var customerId = Guid.NewGuid();
        var cart = new FakeCartBuilder(customerId).WithDefaultItems().Build();

        var command = new CartCheckoutCommand(cart.Id, shippingAddress);

        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(cart.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);
  
        var inventory = new Mock<IInventoryRepository>();
        inventory.Setup(i => i.TryReserveStockAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var order = FakeOrder.FromCart(cart, shippingAddress);
        var orderFactory = new Mock<IOrderFactory>();
        orderFactory.Setup(f => f.CreateFromCart(cart, customerId, It.IsAny<ShippingAddress>()))
            .Returns(order);

        var orderRepo = new Mock<IOrderRepository>();
        var eventPublisher = new Mock<IIntegrationEventPublisher>();

        var checkoutUow = new Mock<ICheckoutUnitOfWork>();
        checkoutUow.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("Simulated commit failure"));

        var orderUow = new Mock<IOrderUnitOfWork>();

        var handler = new CartCheckoutCommandHandler(
            cartRepo.Object, inventory.Object, orderFactory.Object,
            orderRepo.Object, eventPublisher.Object,
            checkoutUow.Object, orderUow.Object
        );

        // Act & Assert
        await FluentActions.Invoking(() => handler.Handle(command, CancellationToken.None))
            .Should().ThrowAsync<Exception>()
            .WithMessage("Simulated commit failure");
    }

    [Fact]
    public async Task Checkout_Fails_WhenCartNotFound()
    {
        // Arrange
        var customerId = Guid.NewGuid();
        var shippingAddress = FakeShippingAddressDto.FakeAddress();
        var cart = new FakeCartBuilder(customerId).Build();
        var command = new CartCheckoutCommand(cart.Id, shippingAddress);
        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(cart.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cart?) null);

        var handler = new CartCheckoutCommandHandler(cartRepo.Object, null!, null!, null!, null!, null!, null!);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartEmptyError>();
    }
}