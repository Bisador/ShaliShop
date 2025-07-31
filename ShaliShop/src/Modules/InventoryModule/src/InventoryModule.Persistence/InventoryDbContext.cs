using InventoryModule.Domain.Inventories.Aggregates;
using InventoryModule.Persistence.Inventories;

namespace InventoryModule.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<Inventory> Inventories => Set<Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("inventory");
        modelBuilder.ApplyConfiguration(new InventoryConfiguration());
    }
}