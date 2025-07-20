using ProductModule.Domain.Products.Aggregates;

namespace ProductModule.Domain.Products.Repository;

public interface IProductRepository
{
    Task<Product?> LoadAsync(Guid id, CancellationToken ct);
    Task SaveAsync(Product item, CancellationToken ct);
}