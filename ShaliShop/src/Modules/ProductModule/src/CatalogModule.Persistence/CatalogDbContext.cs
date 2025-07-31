using CatalogModule.Persistence.Products;
using Shared.Domain;

namespace CatalogModule.Persistence;

public class CatalogDbContext(DbContextOptions<CatalogDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasDefaultSchema("catalog")
            .ApplyConfiguration(new ProductConfiguration());
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