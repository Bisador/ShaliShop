namespace Shop.Domain.Carts.Rules;

public record QuantityMustBeGreaterThanZero(int Quantity) : IBusinessRule
{
    public bool IsBroken() => Quantity <= 0;
    public string Message => "Quantity must be greater than zero.";
}

public record ProductNotFound(int ProductIndex) : IBusinessRule
{
    public bool IsBroken() => ProductIndex == -1;
    public string Message => "Product not found.";
}