using Shop.Application.Orders.Commands.Errors;
using Shop.Domain.Orders.Repository;

namespace Shop.Application.Orders.Commands.Cancel;

public class OrderCancelCommandHandler(
    IOrderRepository orders,
    IShoppingUnitOfWork unitOfWork
) : IRequestHandler<OrderCancelCommand, Result>
{
    public async Task<Result> Handle(OrderCancelCommand command, CancellationToken ct)
    {
        var order = await orders.LoadAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(new OrderNotFoundError(command.OrderId));

        order.Cancel(command.Reason);

        await orders.SaveAsync(order, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}