using OrderModule.Application.Orders.Commands.Errors;
using OrderModule.Domain.Orders.Repository;

namespace OrderModule.Application.Orders.Commands.Confirm;

public class OrderConfirmCommandHandler(
    IOrderRepository orders,
    IShoppingUnitOfWork unitOfWork
) : IRequestHandler<OrderConfirmCommand, Result>
{
    public async Task<Result> Handle(OrderConfirmCommand command, CancellationToken ct)
    {
        var order = await orders.LoadAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(new OrderNotFoundError(command.OrderId)); 

        order.Confirm();

        await orders.SaveAsync(order, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}