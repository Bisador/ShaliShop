namespace CatalogModule.Domain.Products.Exceptions;

public class CannotPublishWithoutNameAndPrice() : DomainException("Cannot publish without name and price.");