using SharedModule.Domain.ValueObjects;
using Shop.Domain.Orders.Enums;
using Shop.Domain.Orders.ValueObjects;

namespace Shop.Application.Tests.TestUtils;
 
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
        var payment = new Payment("TXN001", PaymentMethod.CreditCard, DateTime.UtcNow);
        order.Pay(payment);
        return order;
    }

    public static Order WithStatus(OrderStatus status, out Guid orderId)
    {
        var order = Placed(out orderId);

        if (status == OrderStatus.Paid)
        {
            var payment = new Payment("TXN001", PaymentMethod.CreditCard, DateTime.UtcNow);
            order.Pay(payment);
        }
        else if (status == OrderStatus.Shipped)
        {
            order.Pay(new Payment("TXN002", PaymentMethod.CreditCard, DateTime.UtcNow));
            order.Ship();
        }
        else if (status == OrderStatus.Cancelled)
        {
            order.Cancel("Test cancel");
        }

        return order;
    }
}
