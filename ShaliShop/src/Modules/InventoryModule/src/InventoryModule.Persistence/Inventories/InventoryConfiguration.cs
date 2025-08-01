using InventoryModule.Domain.Inventories.Aggregates;

namespace InventoryModule.Persistence.Inventories;

public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
{
    public void Configure(EntityTypeBuilder<Inventory> builder)
    {
        builder.ToTable("Inventories");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.ProductId)
            .IsRequired();

        builder.Property(i => i.QuantityOnHand)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(i => i.Reserved)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        // Private backing field mapping for low stock threshold
        builder.Property<decimal?>("_lowStockThreshold")
            .HasColumnName("LowStockThreshold")
            .HasColumnType("decimal(18,2)");

        // Private flags for domain state
        builder.Property<bool>("_hasBeenDepleted")
            .HasColumnName("HasBeenDepleted");

        builder.Property<bool>("_lowStockAlertFired")
            .HasColumnName("LowStockAlertFired");

        // Shadow property for concurrency
        builder.Property<byte[]>("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();

        // Optional schema-wide configuration
        builder.HasIndex(i => i.ProductId).IsUnique(false);
        
        builder.Ignore(c => c.DomainEvents);  
        
    }
}