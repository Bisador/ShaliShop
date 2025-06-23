using System.Collections.ObjectModel;
using SharedModule.Domain.ValueObjects;

namespace ProductModule.Domain.Products.Aggregates;

public class ProductVariant
{
    public string Sku { get; private set; }
    public IReadOnlyDictionary<string, string> Options { get; private set; }
    public Money? PriceOverride { get; private set; }

    private ProductVariant()
    {
    }

    public ProductVariant(string sku, Dictionary<string, string> options, Money? priceOverride = null)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentNullException(nameof(sku));

        if (options == null || !options.Any())
            throw new ArgumentException("At least one option is required.");

        Sku = sku;
        Options = new ReadOnlyDictionary<string, string>(options);
        PriceOverride = priceOverride;
    }
}