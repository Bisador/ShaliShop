using InventoryModule.Application;

namespace InventoryModule.Persistence;

public sealed class InventoryUnitOfWork(InventoryDbContext dbContext) : IInventoryUnitOfWork
{
    private readonly InventoryDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CommitAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}