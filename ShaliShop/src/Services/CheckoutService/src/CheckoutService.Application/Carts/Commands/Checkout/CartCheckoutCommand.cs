using CheckoutService.Application.Models;

namespace CheckoutService.Application.Carts.Commands.Checkout;

public record CartCheckoutCommand(Guid CartId, ShippingAddressDto ShippingAddress)
    : ICommand<CartCheckoutResult>;