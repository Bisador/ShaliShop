 
using OrderModule.Application.Orders.Models;

namespace OrderModule.Application.Orders.Commands.OrderPlace;

public record OrderPlaceCommand(
    Guid CustomerId,
    ShippingAddressDto ShippingAddress
) : ICommand<OrderPlacementResult>;