using CheckoutModule.Application.Abstraction;

namespace CheckoutModule.Persistence;

public sealed class CheckoutUnitOfWork(CheckoutDbContext dbContext) : ICheckoutUnitOfWork
{
    private readonly CheckoutDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task CommitAsync(CancellationToken ct)
    {
        await _dbContext.SaveChangesAsync(ct);
    }
}