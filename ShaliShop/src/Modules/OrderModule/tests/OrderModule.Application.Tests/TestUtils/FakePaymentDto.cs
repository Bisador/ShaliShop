using OrderModule.Application.Orders.Commands.OrderPay;

namespace OrderModule.Application.Tests.TestUtils;

public static class FakePaymentDto
{
    public static PaymentDto Valid() =>
        new("tx123", "CreditCard", DateTime.UtcNow);
}