 
using Shop.Application.Orders.Models;

namespace Shop.Application.Orders.Commands.OrderPlace;

public record OrderPlaceCommand(
    Guid CustomerId,
    ShippingAddressDto ShippingAddress
) : ICommand<OrderPlacementResult>;