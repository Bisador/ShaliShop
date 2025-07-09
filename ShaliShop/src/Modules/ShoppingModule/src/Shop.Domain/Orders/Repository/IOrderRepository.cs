using Shop.Domain.Orders.Aggregates;

namespace Shop.Domain.Orders.Repository;

public interface IOrderRepository
{
    Task SaveAsync(Order order, CancellationToken ct);
    Task<Order?> LoadAsync(Guid commandOrderId, CancellationToken ct);
}