 

namespace ProductModule.Domain.Products.Rules;

public class VariantNotFoundException(string sku) : BusinessRuleValidationException($"Variant with SKU '{sku}' not found");