namespace CatalogModule.Api.Models;

public record ProductAddVariantRequest(string Sku, Dictionary<string, string> Options, decimal PriceOverride);