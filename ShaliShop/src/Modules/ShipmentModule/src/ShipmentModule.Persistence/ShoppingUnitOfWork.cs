 
using ShipmentModule.Application;

namespace ShipmentModule.Persistence;

public class ShipmentUnitOfWork : IShipmentUnitOfWork
{
    private readonly ShipmentDbContext _context;

    public ShipmentUnitOfWork(ShipmentDbContext context) => _context = context;

    public Task CommitAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}