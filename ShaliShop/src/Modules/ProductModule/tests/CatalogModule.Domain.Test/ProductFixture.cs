using CatalogModule.Domain.Products.Aggregates;
using SharedModule.Domain.ValueObjects;

namespace CatalogModule.Domain.Test;

public static class ProductFixture
{
    public static Product CreateBasic(
        string name = "Creatine Monohydrate",
        decimal price = 29.99m,
        string category = "Supplements")
    {
        return Product.Create(
            name,
            $"{name} description",
            new Money(price),
            category);
    }

    public static Product CreatePublished(
        string name = "Beta-Alanine",
        decimal price = 19.99m,
        string category = "Performance")
    {
        var product = CreateBasic(name, price, category);
        product.Publish();
        return product;
    }

    public static Product CreateDiscontinued(
        string name = "DMAA Pre-Workout",
        decimal price = 49.99m,
        string category = "Legacy")
    {
        var product = CreateBasic(name, price, category);
        product.Discontinue();
        return product;
    }
}