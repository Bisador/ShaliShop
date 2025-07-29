 
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Enums;
using OrderModule.Domain.Orders.Exceptions;
using OrderModule.Domain.Orders.ValueObjects; 

namespace OrderModule.Domain.Orders.Aggregates;

public sealed class Order : AggregateRoot<Guid>
{
    public Guid CustomerId { get; }
    public OrderStatus Status { get; private set; }
    public DateTime PlacedAt { get; private set; }
    public ShippingAddress ShippingAddress { get; private set; }

    private readonly List<OrderItem> _items = [];
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();

    public Money TotalAmount { get; }
    public Payment? PaymentInfo { get; private set; }
 

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
        CheckRule(new OrderMustHaveAtLeastOneItemException(items));

        return new Order(customerId, items, address);
    }

    public void Pay(Payment payment)
    {
        CheckRule(new OnlyPlacedOrdersCanBePaidException(Status)); 

        PaymentInfo = payment;
        Status = OrderStatus.Paid;

        AddDomainEvent(new OrderPaid(Id, payment.TransactionId));
    }
    
    public void Confirm()
    {
        CheckRule(new OnlyPaidOrdersCanBeConfirmedException(Status));   
        Status = OrderStatus.Confirmed; 
        AddDomainEvent(new OrderConfirmed(Id));
    }


    public void Cancel(string reason)
    {
        CheckRule(new OnlyPlacedOrdersCanBeCancelledException(Status));   
        
        Status = OrderStatus.Cancelled; 
        
        AddDomainEvent(new OrderCancelled(Id, reason));
    }

    public void Ship()
    {
        CheckRule(new OnlyPaidOrdersCanBeShippedException(Status));    

        Status = OrderStatus.Shipped;

        AddDomainEvent(new OrderShipped(Id));
    }

    public void Return(List<OrderItem> returnedItems)
    {
        CheckRule(new OnlyShippedOrdersCanBeReturnedException(Status));   

        Status = OrderStatus.Returned;

        AddDomainEvent(new OrderReturned(Id, returnedItems));
    }

    private Money CalculateTotal()
    { 
        var total = _items.Sum(i => i.UnitPrice.Amount * i.Quantity);
        return new Money(total, "USD");
    } 
}