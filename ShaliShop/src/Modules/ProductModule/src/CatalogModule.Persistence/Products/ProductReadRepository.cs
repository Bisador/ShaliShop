using CatalogModule.Application.Abstraction.Products;
using Shared.Application.Queries;

namespace CatalogModule.Persistence.Products;

public class ProductReadRepository(CatalogDbContext db) : IProductReadRepository
{
    public async Task<ProductDto?> FindByIdAsync(Guid productId, CancellationToken cancellationToken = default)
    {
        return await db.Products
            .Where(p => p.Id == productId)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price.Amount,
                p.IsPublished,
                p.IsDiscontinued))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<ProductDto>> GetAllAsync(IStandardQuery query, CancellationToken cancellationToken = default) =>
        await db.Products
            .ApplyFilter(query)
            .ApplyOrder(query)
            .ApplyPagination(query)
            .Select(p => new ProductDto(
                p.Id,
                p.Name,
                p.Description,
                p.Price.Amount,
                p.IsPublished,
                p.IsDiscontinued))
            .ToListAsync(cancellationToken);

    public async Task<int> GetTotalAsync(IStandardQuery query, CancellationToken cancellationToken = default) =>
        await db.Products
            .ApplyFilter(query)
            .ApplyOrder(query)
            .CountAsync(cancellationToken);
}