using InventoryModule.Domain.Inventories.Aggregates;

namespace InventoryModule.Persistence.Inventories;

public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
{
    public DbSet<Inventory> Inventories => Set<Inventory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("InventoryModule");
        modelBuilder.ApplyConfiguration(new InventoryEntityTypeConfiguration());
    }
}