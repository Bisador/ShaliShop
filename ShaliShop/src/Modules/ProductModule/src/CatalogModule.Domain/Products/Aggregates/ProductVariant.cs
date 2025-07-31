 
using CatalogModule.Domain.Products.Exceptions;

namespace CatalogModule.Domain.Products.Aggregates;

public class ProductVariant
{
    public string Sku { get; private set; } = null!;
    // EF Core maps this
    private Dictionary<string, string> _options = new();
    public IReadOnlyDictionary<string, string> Options => _options;
     
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
        _options = new Dictionary<string, string>(options); // copy for safety
        PriceOverride = priceOverride;
    }
}