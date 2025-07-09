using Shop.Domain.Carts.Aggregates;
using Shop.Domain.Orders.DomainEvents;
using Shop.Domain.Orders.Enums;
using Shop.Domain.Orders.Rules;
using Shop.Domain.Orders.ValueObjects;

namespace Shop.Domain.Orders.Aggregates;

public sealed class Order : AggregateRoot<Guid>
{
    public Guid CustomerId { get; }
    public OrderStatus Status { get; private set; }
    public DateTime PlacedAt { get; private set; }
    public ShippingAddress ShippingAddress { get; private set; } = null!;

    private readonly List<OrderItem> _items = new();
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money TotalAmount { get; } = null!;
    public PaymentInfo? PaymentInfo { get; private set; }

    private Order() : base()
    {
    }

    private Order(Guid customerId, List<OrderItem> items, ShippingAddress address) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        _items.AddRange(items);
        ShippingAddress = address;
        PlacedAt = DateTime.UtcNow;
        TotalAmount = CalculateTotal();
        Status = OrderStatus.Placed;

        AddDomainEvent(new OrderPlaced(Id, CustomerId, items, TotalAmount));
    }

    public static Order Place(Guid customerId, List<OrderItem> items, ShippingAddress address)
    {
        CheckRule(new OrderMustHaveAtLeastOneItem(items));

        return new Order(customerId, items, address);
    }

    public void Pay(PaymentInfo payment)
    {
        if (Status != OrderStatus.Placed)
            throw new InvalidOperationException("Only placed orders can be paid.");

        PaymentInfo = payment;
        Status = OrderStatus.Paid;

        AddDomainEvent(new OrderPaid(Id, payment.TransactionId));
    }

    public void Cancel(string reason)
    {
        if (Status != OrderStatus.Placed)
            throw new InvalidOperationException("Only placed orders can be cancelled.");

        Status = OrderStatus.Cancelled;

        AddDomainEvent(new OrderCancelled(Id, reason));
    }

    public void Ship()
    {
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Only paid orders can be shipped.");

        Status = OrderStatus.Shipped;

        AddDomainEvent(new OrderShipped(Id));
    }

    public void Return(List<OrderItem> returnedItems)
    {
        if (Status != OrderStatus.Shipped)
            throw new InvalidOperationException("Only shipped orders can be returned.");

        Status = OrderStatus.Returned;

        AddDomainEvent(new OrderReturned(Id, returnedItems));
    }

    private Money CalculateTotal()
    { 
        var total = _items.Sum(i => i.UnitPrice.Amount * i.Quantity);
        return new Money(total, "USD");
    }

    public static Order CreateFromCart(Cart cart, Guid customerId, ShippingAddress address)
    {
        if (cart.IsEmpty)
            throw new BusinessRuleValidationException("Cannot create order from empty cart.");

        var orderItems = cart.Items.Select(item =>
            new OrderItem(
                productId: item.ProductId,
                productName: item.ProductName,
                unitPrice: item.UnitPrice,
                quantity: item.Quantity)).ToList();
  
        var order = new Order(
            customerId: customerId,
            address: address,
            items: orderItems);

        order.AddDomainEvent(new OrderPlaced(order.Id, customerId, order.Items, order.TotalAmount));

        return order;
    }
}