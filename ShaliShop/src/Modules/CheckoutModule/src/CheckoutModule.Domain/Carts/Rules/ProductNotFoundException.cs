namespace CheckoutModule.Domain.Carts.Rules;

public class ProductNotFoundException(int productIndex) : DomainException("Product not found.")
{
    public override bool IsBroken() => productIndex == -1; 
}