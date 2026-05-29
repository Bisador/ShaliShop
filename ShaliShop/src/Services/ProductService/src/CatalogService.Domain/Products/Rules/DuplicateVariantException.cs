namespace CatalogService.Domain.Products.Rules;

public class DuplicateVariantException() : DomainException($"SKU must be unique");