using OrderModule.Domain.Orders.Aggregates;
using SharedModule.Domain.ValueObjects;
using OrderModule.Domain.Orders.Enums;
using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Application.Tests.TestUtils;

public static class FakeOrder
{
    public static Order Placed(out Guid orderId)
    {
        var customerId = Guid.NewGuid();
        var address = new ShippingAddress("Tokyo", "1st Ave", "10101", "Kanto", "JP");

        var items = new List<OrderItem>
        {
            new(
                productId: Guid.NewGuid(),
                productName: "L-Glutamine",
                quantity: 2,
                unitPrice: new Money(20, "USD"))
        };

        var order = Order.Place(customerId, items, address);
        orderId = order.Id;
        return order;
    }

    public static Order Paid(out Guid orderId)
    {
        var order = Placed(out orderId);
        var payment =  Payment();
        order.Pay(payment);
        return order;
    }

    private static Payment Payment() => new("TXN001", PaymentMethod.CreditCard, DateTime.UtcNow);

    public static Order WithStatus(OrderStatus status, out Guid orderId)
    {
        var order = Placed(out orderId); 
        var payment =  Payment();
        switch (status)
        {
            case OrderStatus.Paid:
                order.Pay(payment);
                break;
            case OrderStatus.Confirmed:
                order.Pay(payment);
                order.Confirm();
                break;
            case OrderStatus.Shipped:
                order.Pay(payment); 
                order.Ship();
                break;
            case OrderStatus.Cancelled:
                order.Cancel("Test cancel");
                break;
            case OrderStatus.Placed:
                break; 
            default:
                throw new ArgumentOutOfRangeException(nameof(status), status, null);
        }

        return order;
    }
}