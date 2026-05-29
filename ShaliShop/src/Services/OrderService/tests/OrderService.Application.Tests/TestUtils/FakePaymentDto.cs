using OrderService.Application.Orders.Commands.OrderPay;

namespace OrderService.Application.Tests.TestUtils;

public static class FakePaymentDto
{
    public static PaymentDto Valid() =>
        new("tx123", "CreditCard", DateTime.UtcNow);
}