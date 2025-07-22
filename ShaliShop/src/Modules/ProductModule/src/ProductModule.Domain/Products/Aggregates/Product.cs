using ProductModule.Domain.Products.DomainEvents;
using ProductModule.Domain.Products.Exceptions;
using ProductModule.Domain.Products.Rules;
using Shared.Domain;
using SharedModule.Domain.ValueObjects;

namespace ProductModule.Domain.Products.Aggregates;

public sealed class Product : AggregateRoot<Guid>
{
    #region Descriptive

    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public string Category { get; private set; } = null!;

    #endregion

    #region Commercial

    public Money Price { get; private set; } = null!;
    public bool IsPublished { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public bool IsDiscontinued { get; private set; }

    public void Publish()
    {
        if (string.IsNullOrWhiteSpace(Name) || Price.Amount <= 0)
            throw new CannotPublishWithoutNameAndPrice();

        if (IsDiscontinued)
            throw new DiscontinuedProductsCannotBePublished();

        IsPublished = true;
        PublishedAt = DateTime.UtcNow;
        LastModifiedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductPublished(Id));
    }

    public void ChangePrice(Money newPrice)
    {
        if (newPrice.Equals(Price))
            return;

        Price = newPrice;
        LastModifiedAt = DateTime.UtcNow;

        AddDomainEvent(new ProductPriceChanged(Id, newPrice));
    }

    public void Discontinue()
    {
        if (IsDiscontinued)
            return;

        IsDiscontinued = true;
        IsPublished = false;

        AddDomainEvent(new ProductDiscontinued(Id));
    }

    #endregion

    #region Configurable

    #region Variants

    private readonly List<ProductVariant> _variants = [];
    public IReadOnlyCollection<ProductVariant> Variants => _variants.AsReadOnly();

    public void AddVariant(ProductVariant variant)
    {
        if (_variants.Any(v => v.Sku == variant.Sku))
            throw new DuplicateVariantException();

        _variants.Add(variant);
        AddDomainEvent(new ProductVariantAdded(Id, variant.Sku));
    }

    public void RemoveVariant(string sku)
    {
        var variant = _variants.FirstOrDefault(v => v.Sku == sku);
        if (variant == null)
            throw new VariantNotFoundException(sku);

        _variants.Remove(variant);

        AddDomainEvent(new ProductVariantRemoved(Id, sku));
    }

    public Money GetPriceForSku(string sku)
    {
        var variant = _variants.FirstOrDefault(v => v.Sku == sku);
        if (variant == null)
            throw new VariantNotFoundException(sku);

        return variant.PriceOverride ?? Price;
    }

    public ProductVariant? GetVariantBySku(string sku) =>
        _variants.FirstOrDefault(v => v.Sku == sku);

    #endregion

    #endregion

    public DateTime CreatedAt { get; private set; }
    public DateTime? LastModifiedAt { get; private set; }

    private Product()
    {
    }

    private Product(string name, string description, Money price, string category) : base(Guid.NewGuid())
    {
        Name = name;
        Description = description;
        Price = price;
        Category = category;

        CreatedAt = DateTime.UtcNow;
        IsPublished = false;
        IsDiscontinued = false;

        AddDomainEvent(new ProductCreated(Id, Name, Category));
    }

    public static Product Create(string name, string description, Money price, string category)
    { 
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
        ArgumentException.ThrowIfNullOrEmpty(category, nameof(category));
        ArgumentOutOfRangeException.ThrowIfNegative(price.Amount, nameof(price));
        return new Product(name, description, price, category);
    }

    public record Size(string Value)
    {
        public static readonly Size G500 = new("500g");
        public static readonly Size G1000 = new("1kg");

        public override string ToString() => Value;
    }
}