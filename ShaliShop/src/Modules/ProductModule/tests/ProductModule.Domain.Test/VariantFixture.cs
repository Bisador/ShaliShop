using ProductModule.Domain.Products.Aggregates;
using SharedModule.Domain.ValueObjects;

namespace ProductModule.Domain.Test;

public static class VariantFixture
{
    public static ProductVariant Chocolate500G(
        string sku = "CREAT-CHO-500",
        Money? priceOverride = null)
    {
        return new ProductVariant(
            sku,
            new Dictionary<string, string>
            {
                { "Flavor", "Chocolate" },
                { "Size", "500g" }
            },
            priceOverride);
    }

    public static ProductVariant Vanilla1000G(
        string sku = "CREAT-VAN-1000",
        Money? priceOverride = null)
    {
        return new ProductVariant(
            sku,
            new Dictionary<string, string>
            {
                { "Flavor", "Vanilla" },
                { "Size", "1kg" }
            },
            priceOverride);
    }
}