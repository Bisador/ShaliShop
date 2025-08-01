namespace CatalogModule.Domain.Products.Exceptions;

public class DiscontinuedProductsCannotBePublishedException(bool isDiscontinued) : DomainException("Discontinued products cannot be published.")
{
    public override bool IsBroken()
    {
        return isDiscontinued;
    }
}