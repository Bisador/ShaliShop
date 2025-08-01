using CheckoutModule.Domain.Carts.Aggregates;

namespace CheckoutModule.Persistence.Carts;

public sealed class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts", "checkout");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.CustomerId).IsRequired();
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.LastModified).IsRequired();

        builder.OwnsMany(c => c.Items, items =>
        {
            items.WithOwner().HasForeignKey("CartId");
            items.ToTable("CartItems");
            items.HasKey("CartId", "ProductId");

            items.Property(i => i.ProductId).IsRequired();
            items.Property(i => i.ProductName).IsRequired();
            items.Property(i => i.Quantity).IsRequired();

            items.OwnsOne(i => i.UnitPrice, price =>
            {
                price.Property(p => p.Amount)
                    .HasColumnName("UnitPrice")
                    .HasColumnType("decimal(18,2)")
                    .IsRequired();
            });
        });

        builder.Ignore(c => c.DomainEvents);
    }
}