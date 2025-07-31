using CatalogModule.Application;

namespace CatalogModule.Persistence;

public sealed class CatalogUnitOfWork(CatalogDbContext dbContext) : ICatalogUnitOfWork
{
    private readonly CatalogDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CommitAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}