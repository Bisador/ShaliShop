using Shared.Application.Queries;

namespace CatalogService.Application.Abstraction.Products;

public interface IProductReadRepository
{
    Task<ProductDto?> FindByIdAsync(Guid productId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ProductDto>> GetAllAsync(IStandardQuery query, CancellationToken cancellationToken = default);
    Task<int> GetTotalAsync(IStandardQuery query, CancellationToken cancellationToken = default);
}