using SharedModule.Domain.ValueObjects;
using Shop.Application.Orders.Commands.Errors;
using Shop.Application.Orders.Commands.OrderPlace;
using Shop.Application.Tests.TestUtils;
using Shop.Domain.Carts.Aggregates;

namespace Shop.Application.Tests.Orders.Commands.OrderPlace;

public class OrderPlaceCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _customers = new();
    private readonly Mock<ICartRepository> _carts = new();
    private readonly Mock<IOrderRepository> _orders = new();
    private readonly Mock<IInventoryService> _inventory = new();

    private readonly OrderPlaceCommandHandler _handler;

    public OrderPlaceCommandHandlerTests()
    {
        _handler = new OrderPlaceCommandHandler(
            _customers.Object,
            _carts.Object,
            _orders.Object,
            _inventory.Object);
    }

    [Fact]
    public async Task Should_succeed_when_cart_is_valid_and_stock_is_reserved()
    {
        var customer = FakeCustomer.Registered();
        var cart = FakeCart.WithItem("Creatine", productId: Guid.NewGuid(), quantity: 2);

        _customers.Setup(r => r.LoadAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carts.Setup(r => r.LoadForCustomerAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        _inventory.Setup(i => i.TryReserveStockAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var command = new OrderPlaceCommand(customer.Id, FakeShippingAddressDto.Valid());

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value!.OrderId.Should().NotBe(Guid.Empty);

        _orders.Verify(o => o.SaveAsync(It.IsAny<Order>(), It.IsAny<CancellationToken>()), Times.Once);
        _carts.Verify(c => c.SaveAsync(cart, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_fail_when_cart_is_empty()
    {
        var customer = FakeCustomer.Registered();
        var emptyCart = FakeCart.Empty(customer.Id);

        _customers.Setup(c => c.LoadAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carts.Setup(c => c.LoadForCustomerAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(emptyCart);

        var command = new OrderPlaceCommand(customer.Id, FakeShippingAddressDto.Valid());
        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.First().Code.Should().Be(CartEmptyError.ErrorCode);
    }

    [Fact]
    public async Task Should_fail_when_stock_is_unavailable()
    {
        var productId = Guid.NewGuid();

        var cart = FakeCart.WithItem("Whey Protein", productId, 2);
        var customer = FakeCustomer.Registered();

        var inventory = new FakeInventoryService();
        inventory.SetUnavailable(productId);

        _customers.Setup(r => r.LoadAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carts.Setup(r => r.LoadForCustomerAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        var handler = new OrderPlaceCommandHandler(
            _customers.Object,
            _carts.Object,
            _orders.Object,
            inventory);

        var command = new OrderPlaceCommand(customer.Id, FakeShippingAddressDto.Valid());

        var result = await handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors!.First().Code.Should().Be(StockUnavailableError.ErrorCode);
    }

    [Fact]
    public async Task Should_clear_cart_when_order_is_successful()
    {
        var customer = FakeCustomer.Registered();
        var productId = Guid.NewGuid();
        var cart = FakeCart.WithItem("Zinc Tablets", productId, 1);

        _customers.Setup(c => c.LoadAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carts.Setup(c => c.LoadForCustomerAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        _inventory.Setup(i => i.TryReserveStockAsync(productId, It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var command = new OrderPlaceCommand(customer.Id, FakeShippingAddressDto.Valid());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();

        _carts.Verify(c => c.SaveAsync(It.Is<Cart>(c => c.IsEmpty), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Should_return_correct_total_in_order_placement_result()
    {
        var customer = FakeCustomer.Registered();
        var productId = Guid.NewGuid();
        var cart = FakeCart.WithItem("Creatine", productId, quantity: 2,Money.Create(29.99m));

        _customers.Setup(c => c.LoadAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _carts.Setup(c => c.LoadForCustomerAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);

        _inventory.Setup(i => i.TryReserveStockAsync(productId, 2, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success());

        var command = new OrderPlaceCommand(customer.Id, FakeShippingAddressDto.Valid());

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value!.Total.Amount.Should().Be(59.98m); 
    }

}