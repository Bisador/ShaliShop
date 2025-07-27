 
namespace ShippingModule.Persistence;

public class ShippingUnitOfWork(ShipmentDbContext context) : IShippingUnitOfWork
{
    public Task CommitAsync(CancellationToken ct = default)
        => context.SaveChangesAsync(ct);
}