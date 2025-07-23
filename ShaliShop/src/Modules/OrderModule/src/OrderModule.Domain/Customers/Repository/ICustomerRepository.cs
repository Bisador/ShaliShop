using OrderModule.Domain.Customers.Aggregate;

namespace OrderModule.Domain.Customers.Repository;

public interface ICustomerRepository
{
    Task<Customer> LoadAsync(Guid commandCustomerId, CancellationToken ct);
}