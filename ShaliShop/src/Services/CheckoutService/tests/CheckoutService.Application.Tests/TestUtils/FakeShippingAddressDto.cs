using CheckoutService.Application.Models;

namespace CheckoutService.Application.Tests.TestUtils;

public static class FakeShippingAddressDto
{
    public static ShippingAddressDto FakeAddress()
    {
        return new ShippingAddressDto("Tehran", "Azadi St.", "11111", "Tehran", "Iran");
        
    }
}