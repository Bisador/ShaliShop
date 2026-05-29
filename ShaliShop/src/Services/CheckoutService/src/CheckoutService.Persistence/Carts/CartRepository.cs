using CheckoutService.Domain.Carts.Aggregates;
using CheckoutService.Domain.Carts.Repository;

namespace CheckoutService.Persistence.Carts;

public sealed class CartRepository(CheckoutDbContext db) : ICartRepository
{
    public async Task<Cart?> LoadAsync(Guid id, CancellationToken ct)
    {
        return await db.Set<Cart>()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public Task SaveAsync(Cart cart, CancellationToken ct)
    {
        db.Update(cart);
        return Task.CompletedTask;
    }
}