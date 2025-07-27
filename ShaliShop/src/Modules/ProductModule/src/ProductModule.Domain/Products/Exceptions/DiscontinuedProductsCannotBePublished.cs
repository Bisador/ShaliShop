namespace ProductModule.Domain.Products.Exceptions;

public class DiscontinuedProductsCannotBePublished() : BusinessRuleValidationException("Discontinued products cannot be published.");