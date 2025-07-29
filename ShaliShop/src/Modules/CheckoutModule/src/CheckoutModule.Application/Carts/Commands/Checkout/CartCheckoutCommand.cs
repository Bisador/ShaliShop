using CheckoutModule.Application.Models;

namespace CheckoutModule.Application.Carts.Commands.Checkout;

public record CartCheckoutCommand(Guid CartId, ShippingAddressDto ShippingAddress)
    : ICommand<CartCheckoutResult>;