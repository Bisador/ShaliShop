 
using ShippingModule.Application;

namespace ShippingModule.Persistence;

public class ShipmentUnitOfWork : IShippingUnitOfWork
{
    private readonly ShipmentDbContext _context;

    public ShipmentUnitOfWork(ShipmentDbContext context) => _context = context;

    public Task CommitAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}