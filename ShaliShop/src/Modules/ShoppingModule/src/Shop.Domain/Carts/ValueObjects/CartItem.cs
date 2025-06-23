using Shop.Domain.Carts.Rules;

namespace Shop.Domain.Carts.ValueObjects;

public class CartItem : ValueObject
{
    public Guid ProductId { get; }
    public string ProductName { get; }
    public int Quantity { get; private set; }
    public Money UnitPrice { get; }

    public CartItem(Guid productId, string productName, int quantity, Money unitPrice)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitPrice = unitPrice;
    }

    public void IncreaseQuantity(int by)
    {
        CheckRule(new QuantityMustBeGreaterThanZero(by));
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