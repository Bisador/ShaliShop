using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Carts.Commands.Checkout;

public record CartCheckoutResult(Guid OrderId, Money TotalAmount);