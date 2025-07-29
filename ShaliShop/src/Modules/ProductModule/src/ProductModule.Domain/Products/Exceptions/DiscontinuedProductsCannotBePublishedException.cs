namespace ProductModule.Domain.Products.Exceptions;

public class DiscontinuedProductsCannotBePublishedException(bool isDiscontinued) : BusinessRuleValidationException("Discontinued products cannot be published.")
{
    public override bool IsBroken()
    {
        return isDiscontinued;
    }
}