using CheckoutModule.Domain.Carts.Rules;

namespace CheckoutModule.Domain.Carts.ValueObjects;

public class CartItem : ValueObject
{
    public Guid ProductId { get; private set;}
    public string ProductName { get; private set;}
    public decimal Quantity { get; private set; }
    public Money UnitPrice { get; private set;}
    
    // 👇 THIS is required by EF
    private CartItem() { }

    public CartItem(Guid productId, string productName, decimal quantity, Money unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void UpdateQuantity(decimal by)
    {
        CheckRule(new QuantityMustBeGreaterThanZeroException(by));
        Quantity = by;
    }

    public void IncreaseQuantity(decimal by)
    {
        CheckRule(new QuantityMustBeGreaterThanZeroException(by));
        Quantity += by;
    }

    public decimal LineTotal => UnitPrice.Amount * Quantity;

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return ProductId;
        yield return ProductName;
        yield return Quantity;
        yield return UnitPrice;
    }
}