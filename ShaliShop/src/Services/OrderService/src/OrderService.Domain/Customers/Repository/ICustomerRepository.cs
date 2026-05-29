using OrderService.Domain.Customers.Aggregate;

namespace OrderService.Domain.Customers.Repository;

public interface ICustomerRepository
{
    Task<Customer> LoadAsync(Guid commandCustomerId, CancellationToken ct);
}