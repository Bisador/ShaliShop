using CatalogModule.Domain.Products.Aggregates;
using CatalogModule.Domain.Products.DomainEvents;
using CatalogModule.Domain.Products.Exceptions;
using CatalogModule.Domain.Products.Rules;
using SharedModule.Domain.ValueObjects;

namespace CatalogModule.Domain.Test;

public class ProductTests
{
    [Fact]
    public void Creating_product_should_initialize_state_and_raise_ProductCreated()
    {
        var product = ProductFixture.CreateBasic("Whey Protein", 59.99m, "Supplements");

        product.Name.Should().Be("Whey Protein");
        product.Category.Should().Be("Supplements");
        product.Price.Amount.Should().Be(59.99m);
        product.IsPublished.Should().BeFalse();

        product.Events.OfType<ProductCreated>().Single().Should().Match<ProductCreated>(e =>
            e.ProductId == product.Id &&
            e.Name == "Whey Protein" &&
            e.Category == "Supplements");
    }

    [Fact]
    public void Publishing_product_should_set_flags_and_raise_ProductPublished()
    {
        var product = ProductFixture.CreateBasic("Creatine", 34.50m, "Performance");

        product.Publish();

        product.IsPublished.Should().BeTrue();
        product.PublishedAt.Should().NotBeNull();

        product.Events.OfType<ProductPublished>().Single().ProductId.Should().Be(product.Id);
    }

    [Fact]
    public void Publishing_product_without_name_or_price_should_throw()
    {
        var product = Product.Create("", "Invalid", new Money(0), "Test");

        FluentActions.Invoking(() => product.Publish())
            .Should().Throw<CannotPublishWithoutNameAndPrice>();
    }

    [Fact]
    public void Changing_price_should_update_and_raise_ProductPriceChanged()
    {
        var product = ProductFixture.CreateBasic("L-Carnitine", 20, "Metabolism");

        product.ChangePrice(new Money(25));

        product.Price.Amount.Should().Be(25);

        var @event = product.Events.OfType<ProductPriceChanged>().Single();
        @event.ProductId.Should().Be(product.Id);
        @event.NewPrice.Amount.Should().Be(25);
    }

    [Fact]
    public void Discontinuing_product_should_mark_unavailable_and_raise_event()
    {
        var product = ProductFixture.CreateBasic("Zinc", 12, "Micronutrients");

        product.Discontinue();

        product.IsDiscontinued.Should().BeTrue();
        product.IsPublished.Should().BeFalse();

        product.Events.OfType<ProductDiscontinued>().Single().ProductId.Should().Be(product.Id);
    }
    
    [Fact]
    public void Adding_variant_should_store_it_and_raise_ProductVariantAdded()
    {
        var product = ProductFixture.CreateBasic("Creatine", 34.50m, "Performance");

        var variant = new ProductVariant(
            sku: "CREAT-CHO-500",
            options: new Dictionary<string, string>
            {
                { "Flavor", "Chocolate" },
                { "Size", "500g" }
            },
            priceOverride: new Money(36.00m)
        );

        product.AddVariant(variant);

        product.Variants.Should().ContainSingle(v => v.Sku == "CREAT-CHO-500");
        product.Events.OfType<ProductVariantAdded>()
            .Single().Sku.Should().Be("CREAT-CHO-500");
    }
    
    [Fact]
    public void Adding_duplicate_sku_should_throw_and_not_store_variant()
    {
        var product = ProductFixture.CreateBasic();
        var variant = VariantFixture.Chocolate500G();

        product.AddVariant(variant);

        Action act = () => product.AddVariant(VariantFixture.Chocolate500G());
    
        act.Should().Throw<DuplicateVariantException>();

        product.Variants.Count.Should().Be(1);
    }

    [Fact]
    public void Removing_variant_should_delete_it_and_raise_ProductVariantRemoved()
    {
        var product = ProductFixture.CreateBasic();
        var variant = VariantFixture.Chocolate500G();

        product.AddVariant(variant);
        product.RemoveVariant(variant.Sku);

        product.Variants.Should().BeEmpty();
        product.Events.OfType<ProductVariantRemoved>()
            .Single().Sku.Should().Be(variant.Sku);
    }
    
    [Fact]
    public void Removing_nonexistent_variant_should_throw()
    {
        var product = ProductFixture.CreateBasic();

        FluentActions.Invoking(() => product.RemoveVariant("UNKNOWN-SKU"))
            .Should().Throw<VariantNotFoundException>();
    }


}
