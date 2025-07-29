namespace CheckoutModule.Domain.Carts.Rules;

public class ProductNotFoundException(int productIndex) : BusinessRuleValidationException("Product not found.")
{
    public override bool IsBroken() => productIndex == -1; 
}