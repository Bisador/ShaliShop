using Shared.Domain;

namespace Shop.Application.Tests.TestUtils;

public static class FakeCustomer
{
    public static Customer Registered(
        string? fullName = null,
        string? email = null)
    {
        var customer = Customer.Register(
            fullName ?? "Jane Doe",
            email ?? "jane@example.com");
        
        return customer;
    }

    public static Customer Registered(Guid fixedId, string? name = null, string? email = null)
    {
        var customer = Customer.Register(
            name ?? "Ali Turing",
            email ?? "ali@example.com");

        // Simulate fixed ID for test consistency (uses reflection to set private base class prop)
        typeof(AggregateRoot<Guid>)
            .GetProperty("Id")!
            .SetValue(customer, fixedId);

        return customer;
    }
}