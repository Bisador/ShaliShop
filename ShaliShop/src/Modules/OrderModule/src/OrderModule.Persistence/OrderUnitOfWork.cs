namespace OrderModule.Persistence;

public class OrderUnitOfWork(OrderDbContext context) : IOrderUnitOfWork
{
    public Task CommitAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}