using Shop.Domain.Orders.Aggregates;

namespace Shop.Persistence;

public class ShoppingDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}