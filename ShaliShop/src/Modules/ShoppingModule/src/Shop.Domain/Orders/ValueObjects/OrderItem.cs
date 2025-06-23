namespace Shop.Domain.Orders.ValueObjects;

public class OrderItem : ValueObject
{
    public Guid ProductId { get; }
    public string ProductName { get; } = string.Empty;
    public decimal Quantity { get; }
    public Money UnitPrice { get; }

    private OrderItem()
    {
    } // For EF

    public OrderItem(Guid productId, string productName, decimal quantity, Money unitPrice) : this()
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be positive.");

        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public decimal TotalPrice => UnitPrice.Amount * Quantity;
    
    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ProductId;
        yield return ProductName;
        yield return Quantity;
        yield return UnitPrice;
    }
}