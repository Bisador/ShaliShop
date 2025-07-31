using CatalogModule.Application.Abstraction.Products;

namespace CatalogModule.Application.Products.Queries.GetAll;

public class ProductGetAllQueryHandler(IProductReadRepository repository) : IRequestHandler<ProductGetAllQuery, PaginatedResult<ProductDto>>
{
    public async Task<PaginatedResult<ProductDto>> Handle(ProductGetAllQuery query, CancellationToken ct)
    {
        var items = await repository.GetAllAsync(query, ct);
        var total = await repository.GetTotalAsync(query, ct);
        return new PaginatedResult<ProductDto>(items, total, query.Page, query.PageSize);
    }
}