namespace ProductModule.Domain.Products.Exceptions;

public class CannotPublishWithoutNameAndPrice() : BusinessRuleValidationException("Cannot publish without name and price.");