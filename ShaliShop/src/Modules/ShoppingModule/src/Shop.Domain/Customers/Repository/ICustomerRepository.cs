using Shop.Domain.Customers.Aggregate;

namespace Shop.Domain.Customers.Repository;

public interface ICustomerRepository
{
    Task<Customer> LoadAsync(Guid commandCustomerId, CancellationToken ct);
}