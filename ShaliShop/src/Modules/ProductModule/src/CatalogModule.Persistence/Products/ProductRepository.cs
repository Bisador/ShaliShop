using CatalogModule.Domain.Products.Repository;

namespace CatalogModule.Persistence.Products;

public class ProductRepository(CatalogDbContext context) : IProductRepository
{
    public Task<Product?> LoadAsync(Guid id, CancellationToken ct) =>
        context.Products
            .Include(p => p.Variants)
            .FirstOrDefaultAsync(p => p.Id == id, ct);

    public Task SaveAsync(Product product, CancellationToken ct)
    {
        context.Update(product);
        return Task.CompletedTask;
    }
}