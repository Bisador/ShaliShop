using Shop.Application.Orders.Commands.Cancel;
using Shop.Application.Orders.Commands.Errors;
using Shop.Application.Tests.TestUtils;
using Shop.Domain.Orders.DomainEvents;
using Shop.Domain.Orders.Enums;

namespace Shop.Application.Tests.Orders.Commands;

public class OrderCancelCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orders = new();
    private readonly Mock<IShoppingUnitOfWork> _unitOfWork = new();
    private readonly OrderCancelCommandHandler _handler;

    public OrderCancelCommandHandlerTests()
    {
        _handler = new OrderCancelCommandHandler(_orders.Object, _unitOfWork.Object);
    }
    
    [Fact]
    public async Task Should_fail_if_order_not_found()
    {
        var command = new OrderCancelCommand(Guid.NewGuid(), "Changed my mind");

        _orders.Setup(r => r.LoadAsync(command.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<OrderNotFoundError>();
    }

    [Fact]
    public async Task Should_fail_if_order_is_not_placed()
    {
        var order = FakeOrder.Paid(out var orderId); // Status: Paid

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var command = new OrderCancelCommand(orderId, "Requested refund");

        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        await act.Should().ThrowAsync<InvalidOperationException>()
            .WithMessage("Only placed orders can be cancelled.");
    }

    [Fact]
    public async Task Should_cancel_order_and_commit()
    {
        var order = FakeOrder.Placed(out var orderId); // Status: Placed
        var command = new OrderCancelCommand(orderId, "No longer needed");

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Cancelled);

        _orders.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Should_raise_OrderCancelled_event()
    {
        var order = FakeOrder.Placed(out var orderId);
        var reason = "Item out of stock";

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        await _handler.Handle(new OrderCancelCommand(orderId, reason), CancellationToken.None);

        order.Events.Any(e =>
            e is OrderCancelled oc &&
            oc.OrderId == orderId &&
            oc.Reason == reason
        ).Should().BeTrue();
    }

}
