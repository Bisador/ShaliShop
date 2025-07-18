using Shop.Application.Orders.Commands.Confirm;
using Shop.Application.Orders.Commands.Errors;
using Shop.Application.Tests.TestUtils;
using Shop.Domain.Orders.DomainEvents;
using Shop.Domain.Orders.Enums;

namespace Shop.Application.Tests.Orders.Commands;

public class OrderConfirmCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orders = new();
    private readonly Mock<IShoppingUnitOfWork> _unitOfWork = new();
    private readonly OrderConfirmCommandHandler _handler;

    public OrderConfirmCommandHandlerTests()
    {
        _handler = new OrderConfirmCommandHandler(_orders.Object, _unitOfWork.Object);
    }

    [Fact]
    public async Task Should_fail_if_order_not_found()
    {
        var command = new OrderConfirmCommand(Guid.NewGuid());

        _orders.Setup(r => r.LoadAsync(command.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?) null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<OrderNotFoundError>();
    }

    [Fact]
    public async Task Should_fail_if_order_is_not_paid()
    {
        var order = FakeOrder.Placed(out var orderId); // Status: Placed

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var command = new OrderConfirmCommand(orderId);
        var act = () => _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Only paid orders can be confirmed.");
    }

    [Fact]
    public async Task Should_confirm_paid_order_and_commit()
    {
        var order = FakeOrder.Paid(out var orderId); // Status: Paid

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var result = await _handler.Handle(new OrderConfirmCommand(orderId), CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Confirmed);

        _orders.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_raise_OrderConfirmed_event_on_success()
    {
        var order = FakeOrder.Paid(out var orderId);

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        await _handler.Handle(new OrderConfirmCommand(orderId), CancellationToken.None);

        order.Events.Any(e =>
            e is OrderConfirmed confirmed &&
            confirmed.OrderId == orderId).Should().BeTrue();
    }
}