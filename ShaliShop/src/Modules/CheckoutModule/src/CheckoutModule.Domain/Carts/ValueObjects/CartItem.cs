using CheckoutModule.Domain.Carts.Rules;

namespace CheckoutModule.Domain.Carts.ValueObjects;

public class CartItem : ValueObject
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public decimal Quantity { get; private set; }
    public Money UnitPrice { get; }

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