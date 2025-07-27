using System.Collections.ObjectModel;
using ProductModule.Domain.Products.Exceptions;

namespace ProductModule.Domain.Products.Aggregates;

public class ProductVariant
{
    public string Sku { get; private set; } = null!;
    public IReadOnlyDictionary<string, string> Options { get; private set; } = null!;
    public Money? PriceOverride { get; private set; }

    private ProductVariant()
    {
    }

    public ProductVariant(string sku, Dictionary<string, string> options, Money? priceOverride = null)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentNullException(nameof(sku));

        if (options is null || options.Count == 0)
            throw new AtLeastOneOptionIsRequiredException();

        Sku = sku;
        Options = new ReadOnlyDictionary<string, string>(options);
        PriceOverride = priceOverride;
    }
}