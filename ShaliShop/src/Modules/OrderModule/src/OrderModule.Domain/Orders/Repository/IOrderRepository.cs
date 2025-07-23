using OrderModule.Domain.Orders.Aggregates;

namespace OrderModule.Domain.Orders.Repository;

public interface IOrderRepository
{
    Task SaveAsync(Order order, CancellationToken ct);
    Task<Order?> LoadAsync(Guid id, CancellationToken ct);
}