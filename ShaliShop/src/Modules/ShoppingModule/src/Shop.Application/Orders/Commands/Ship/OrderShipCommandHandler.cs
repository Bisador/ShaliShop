using Shop.Application.Orders.Commands.Errors;
using Shop.Domain.Orders.Repository;

namespace Shop.Application.Orders.Commands.Ship;

public class OrderShipCommandHandler(
    IOrderRepository orders,
    IShoppingUnitOfWork unitOfWork
) : IRequestHandler<OrderShipCommand, Result>
{
    public async Task<Result> Handle(OrderShipCommand command, CancellationToken ct)
    {
        var order = await orders.LoadAsync(command.OrderId, ct);
        if (order is null)
            return Result.Failure(new OrderNotFoundError(command.OrderId));

        order.Ship();

        await orders.SaveAsync(order, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}