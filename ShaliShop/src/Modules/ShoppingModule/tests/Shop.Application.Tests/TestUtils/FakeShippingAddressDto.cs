using Shop.Application.Orders.Models;

namespace Shop.Application.Tests.TestUtils;

public static class FakeShippingAddressDto
{
    public static ShippingAddressDto Valid() => new()
    {
        Street = "123 Test St",
        City = "Testville",
        ZipCode = "T1234",
        Country = "GB"
    };

    public static ShippingAddressDto WithMissingCity() => new()
    {
        Street = "123 Test St",
        City = "",
        ZipCode = "T1234",
        Country = "GB"
    };
}