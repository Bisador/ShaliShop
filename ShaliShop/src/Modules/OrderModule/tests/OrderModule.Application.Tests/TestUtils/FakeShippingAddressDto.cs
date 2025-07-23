using OrderModule.Application.Orders.Models;

namespace OrderModule.Application.Tests.TestUtils;

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