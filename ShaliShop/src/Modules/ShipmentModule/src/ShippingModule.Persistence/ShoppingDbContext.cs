using ShippingModule.Domain.Shipments.Aggregates;

namespace ShippingModule.Persistence;

public class ShipmentDbContext : DbContext
{
    public DbSet<Shipment> Orders { get; set; }
}