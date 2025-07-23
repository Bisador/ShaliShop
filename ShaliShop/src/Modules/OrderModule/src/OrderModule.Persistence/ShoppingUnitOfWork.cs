 
using OrderModule.Application;

namespace OrderModule.Persistence;

public class ShoppingUnitOfWork : IShoppingUnitOfWork
{
    private readonly ShoppingDbContext _context;

    public ShoppingUnitOfWork(ShoppingDbContext context) => _context = context;

    public Task CommitAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}