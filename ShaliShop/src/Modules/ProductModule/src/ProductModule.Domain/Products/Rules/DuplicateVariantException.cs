namespace ProductModule.Domain.Products.Rules;

public class DuplicateVariantException() : BusinessRuleValidationException($"SKU must be unique");