using OrderModule.Domain.Orders.Aggregates; 

namespace OrderModule.Persistence;

public class ShoppingDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}