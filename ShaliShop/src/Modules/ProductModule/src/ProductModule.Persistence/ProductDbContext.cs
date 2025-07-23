using ProductModule.Persistence.Products;
using Shared.Domain;

namespace ProductModule.Persistence;

public class ProductDbContext(DbContextOptions<ProductDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("ProductModule")
            .ApplyConfiguration(new ProductEntityTypeConfiguration()); 
    }

    public override Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is not IAuditableEntity product) continue;
            if (entry.State == EntityState.Added)
                product.CreatedAt = DateTime.UtcNow;
            else if (entry.State == EntityState.Modified)
                product.LastModifiedAt = DateTime.UtcNow;
        }

        return base.SaveChangesAsync(ct);
    }
}