using ShippingService.Domain.Shipments.Aggregates;

namespace ShippingService.Persistence;

public class ShipmentDbContext : DbContext
{
    public DbSet<Shipment> Orders { get; set; }
}