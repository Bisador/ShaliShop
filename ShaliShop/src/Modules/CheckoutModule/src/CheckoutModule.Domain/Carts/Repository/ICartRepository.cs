using CheckoutModule.Domain.Carts.Aggregates;

namespace CheckoutModule.Domain.Carts.Repository;

public interface ICartRepository
{
    Task<Cart> LoadForCustomerAsync(Guid commandCustomerId, CancellationToken ct);
    Task SaveAsync(Cart item, CancellationToken ct);
}