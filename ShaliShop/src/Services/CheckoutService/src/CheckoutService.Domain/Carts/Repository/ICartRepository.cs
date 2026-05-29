using CheckoutService.Domain.Carts.Aggregates;

namespace CheckoutService.Domain.Carts.Repository;

public interface ICartRepository
{
    Task<Cart?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Cart item, CancellationToken ct);
}