namespace CheckoutModule.Domain.Carts.Rules;

public class QuantityMustBeGreaterThanZeroException(decimal quantity) : DomainException("Quantity must be greater than zero.")
{
    public override bool IsBroken() => quantity <= 0; 
}