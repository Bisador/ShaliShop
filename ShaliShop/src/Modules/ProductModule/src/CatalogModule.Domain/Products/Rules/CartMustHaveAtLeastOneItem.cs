 

namespace CatalogModule.Domain.Products.Rules;

public class VariantNotFoundException(string sku) : DomainException($"Variant with SKU '{sku}' not found");