using Shop.Application.Orders.Commands.OrderPay;

namespace Shop.Application.Tests.TestUtils;

public static class FakePaymentDto
{
    public static PaymentDto Valid() =>
        new("tx123", "CreditCard", DateTime.UtcNow);
}