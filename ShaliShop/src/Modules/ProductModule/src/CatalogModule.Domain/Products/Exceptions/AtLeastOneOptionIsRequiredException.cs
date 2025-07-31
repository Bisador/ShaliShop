namespace CatalogModule.Domain.Products.Exceptions;

public class AtLeastOneOptionIsRequiredException() : BusinessRuleValidationException("At least one option is required.");