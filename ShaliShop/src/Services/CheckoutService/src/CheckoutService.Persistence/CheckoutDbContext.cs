using CheckoutService.Domain.Carts.Aggregates;

namespace CheckoutService.Persistence;

public class CheckoutDbContext(DbContextOptions<CheckoutDbContext> options) : DbContext(options)
{
    public DbSet<Cart> Carts => Set<Cart>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("checkout"); 
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CheckoutDbContext).Assembly);
    }
}