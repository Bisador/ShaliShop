using Shared.Persistence.Converters;

namespace ProductModule.Persistence.Products;

public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(128);
        builder.Property(p => p.Description).HasMaxLength(512);
        builder.Property(p => p.Category).IsRequired();
        builder.Property(p => p.IsPublished);
        builder.Property(p => p.IsDiscontinued);
        builder.Property(p => p.CreatedAt);
        builder.Property(p => p.LastModifiedAt);
        builder.Property(p => p.PublishedAt);

        builder.OwnsOne(p => p.Price, price =>
        {
            price.Property(p => p.Amount).HasColumnName("PriceAmount");
            price.Property(p => p.Currency).HasColumnName("PriceCurrency");
        });

        builder.OwnsMany(p => p.Variants, variants =>
        {
            variants.WithOwner().HasForeignKey("ProductId");
            variants.Property(v => v.Sku).IsRequired();
            variants.Property(v => v.Options)
                .HasConversion(new JsonValueConverter<Dictionary<string, string>>());

            variants.OwnsOne(v => v.PriceOverride, overridePrice =>
            {
                overridePrice.Property(p => p.Amount).HasColumnName("PriceOverrideAmount");
                overridePrice.Property(p => p.Currency).HasColumnName("PriceOverrideCurrency");
            });
            variants.ToTable("ProductVariants");
        });

        builder.Property<byte[]>("RowVersion")
            .IsRowVersion()
            .IsConcurrencyToken();
    }
}