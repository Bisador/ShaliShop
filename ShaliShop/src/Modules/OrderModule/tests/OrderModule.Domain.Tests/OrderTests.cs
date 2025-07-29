using Shared.Domain;
using SharedModule.Domain.ValueObjects;
using OrderModule.Domain.Orders.Aggregates;
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Enums;
using OrderModule.Domain.Orders.Exceptions;
using OrderModule.Domain.Orders.ValueObjects; 

namespace OrderModule.Domain.Tests;

public class OrderTests
{
    [Fact]
    public void Placing_order_with_no_items_should_throw()
    {
        var customerId = Guid.NewGuid();
        var shipping = new ShippingAddress("Tehran", "Valiasr St", "Zip123");

        Action act = () => Order.Place(customerId, [], shipping);

        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("*at least one item*"); // Adjust to your rule’s message
    }

    [Fact]
    public void Order_total_should_match_sum_of_items()
    {
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), "Kettlebell", 30m, Money.From(2)), // 60
            new(Guid.NewGuid(), "Pull-up Bar", 80m, Money.From(1)) // 80
        };
        var shipping = new ShippingAddress("Tehran", "Valiasr St", "Zip123");
        var order = Order.Place(Guid.NewGuid(), items, shipping);

        order.TotalAmount.Amount.Should().Be(140m);
    }

    [Fact]
    public void Paying_order_should_set_status_and_emit_OrderPaid()
    {
        var customerId = Guid.NewGuid();
        var shipping = new ShippingAddress("Dallas", "Main St", "75201");
        var items = new List<OrderItem>
        {
            new(Guid.NewGuid(), "Power Rack", 1, Money.From(500m))
        };
        var order = Order.Place(customerId, items, shipping);

        var paymentInfo = new Payment("txn-123", PaymentMethod.Cash, DateTime.UtcNow);
        order.Pay(paymentInfo);

        order.Status.Should().Be(OrderStatus.Paid);
        order.PaymentInfo.Should().BeEquivalentTo(paymentInfo);

        order.Events.Any(e =>
            e is OrderPaid paid && paid.OrderId == order.Id && paid.TransactionId == paymentInfo.TransactionId)
            .Should().BeTrue();
    }

    [Fact]
    public void Paying_cancelled_order_should_throw()
    {
        ShippingAddress shipping = new("Dallas", "Main St", "75201");
        List<OrderItem> items =
        [
            new(Guid.NewGuid(), "Power Rack", 1, Money.From(500m))
        ];
        var order = Order.Place(Guid.NewGuid(), items, shipping);
        order.Cancel("Out of stock");

        var paymentInfo = new Payment("txn-123", PaymentMethod.Cash, DateTime.UtcNow);
        var act = () => order.Pay(paymentInfo);
        act.Should().Throw<OnlyPlacedOrdersCanBePaidException>();
    }
}