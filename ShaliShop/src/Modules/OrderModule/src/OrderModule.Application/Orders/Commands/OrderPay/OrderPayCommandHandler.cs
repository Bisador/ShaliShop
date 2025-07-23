using OrderModule.Application.Orders.Commands.Errors;
using OrderModule.Domain.Orders.Enums;
using OrderModule.Domain.Orders.Repository;
using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Application.Orders.Commands.OrderPay;

public class OrderPayCommandHandler(
    IOrderRepository orders,
    IShoppingUnitOfWork unitOfWork) : IRequestHandler<OrderPayCommand, Result>
{
    public async Task<Result> Handle(OrderPayCommand command, CancellationToken ct)
    {
        var order = await orders.LoadAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(new OrderNotFoundError(command.OrderId));

        var methodParsed = Enum.TryParse<PaymentMethod>(command.Payment.Method, out var method);
        if (!methodParsed)
            return Result.Failure(new InvalidPaymentMethodError());

        var payment = new Payment(
            command.Payment.TransactionId,
            method,
            command.Payment.PaidAt
        );
        order.Pay(payment);

        await orders.SaveAsync(order, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}