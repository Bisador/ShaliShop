using SharedService.Domain.ValueObjects;

namespace CheckoutService.Application.Carts.Commands.Checkout;

public record CartCheckoutResult(Guid OrderId, Money TotalAmount);