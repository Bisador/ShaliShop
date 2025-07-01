 
using Shop.Domain.Orders.ValueObjects;

namespace Shop.Application.Orders.Models;

public static class MappingConfig
{
    public static void RegisterMappings()
    {
        TypeAdapterConfig<ShippingAddressDto, ShippingAddress>
            .NewConfig()
            .MapToConstructor(true);

        // Add more mappings here...
    }
}