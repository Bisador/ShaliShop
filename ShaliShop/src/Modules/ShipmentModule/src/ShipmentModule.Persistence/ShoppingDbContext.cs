using ShipmentModule.Domain.Shipments.Aggregates;

namespace ShipmentModule.Persistence;

public class ShipmentDbContext : DbContext
{
    public DbSet<Shipment> Orders { get; set; }
}