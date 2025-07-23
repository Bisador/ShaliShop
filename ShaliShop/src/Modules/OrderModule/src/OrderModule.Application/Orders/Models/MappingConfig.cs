 
using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Application.Orders.Models;

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