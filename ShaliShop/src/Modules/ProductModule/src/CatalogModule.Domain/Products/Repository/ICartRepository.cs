using CatalogModule.Domain.Products.Aggregates;

namespace CatalogModule.Domain.Products.Repository;

public interface IProductRepository
{
    Task<Product?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Product item, CancellationToken ct);
}