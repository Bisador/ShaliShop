using InventoryService.Domain.Inventories.Aggregates;
using InventoryService.Persistence.Inventories;

namespace InventoryService.Persistence;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<Inventory> Inventories => Set<Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("inventory");
        modelBuilder.ApplyConfiguration(new InventoryConfiguration());
    }
}