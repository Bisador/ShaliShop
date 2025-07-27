using OrderModule.Domain.Orders.Aggregates; 

namespace OrderModule.Persistence;

public class OrderDbContext : DbContext
{
    public DbSet<Order> Orders { get; set; }
}