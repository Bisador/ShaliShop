 
using Shop.Application.Orders.Commands.OrderPay;
using Shop.Application.Tests.TestUtils;
using Shop.Domain.Orders.Enums;

namespace Shop.Application.Tests.Orders.Commands;

public class OrderPayCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _orders = new();
    private readonly Mock<IShoppingUnitOfWork> _unitOfWork = new();
    private readonly OrderPayCommandHandler _handler;

    public OrderPayCommandHandlerTests()
    {
        _handler = new OrderPayCommandHandler(_orders.Object, _unitOfWork.Object);
    }
    
    [Fact]
    public async Task Should_fail_when_order_is_not_found()
    {
        var command = new OrderPayCommand(Guid.NewGuid(), FakePaymentDto.Valid());
    
        _orders.Setup(r => r.LoadAsync(command.OrderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Order?)null);

        var result = await _handler.Handle(command, default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThan(0);
    }
    
    [Fact]
    public async Task Should_fail_when_payment_method_is_invalid()
    {
        var order = FakeOrder.Placed(out var orderId);
        var dto = new PaymentDto("tx123", "INVALID_METHOD", DateTime.UtcNow);

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var result = await _handler.Handle(new OrderPayCommand(orderId, dto), default);

        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCountGreaterThan(0); 
    }
    
    [Fact]
    public async Task Should_mark_order_as_paid_and_commit()
    {
        var order = FakeOrder.Placed(out var orderId);
        var dto = new PaymentDto("tx123", "CreditCard", DateTime.UtcNow);

        _orders.Setup(r => r.LoadAsync(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(order);

        var result = await _handler.Handle(new OrderPayCommand(orderId, dto), default);

        result.IsSuccess.Should().BeTrue();
        order.Status.Should().Be(OrderStatus.Paid);

        _orders.Verify(r => r.SaveAsync(order, It.IsAny<CancellationToken>()), Times.Once);
        _unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

   

}