using Shop.Domain.Carts.Aggregates;

namespace Shop.Domain.Carts.Repository;

public interface ICartRepository
{
    Task<Cart> LoadForCustomerAsync(Guid commandCustomerId, CancellationToken ct);
    Task SaveAsync(object cart, CancellationToken ct);
}