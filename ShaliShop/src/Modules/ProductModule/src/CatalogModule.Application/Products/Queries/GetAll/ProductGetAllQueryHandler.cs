using CatalogModule.Application.Abstraction.Products; 

namespace CatalogModule.Application.Products.Queries.GetAll;

public class ProductGetAllQueryHandler(IProductReadRepository repository) : IRequestHandler<ProductGetAllQuery, PaginatedResult<ProductDto>>
{
    private readonly IProductReadRepository _repository = repository;

    public async Task<PaginatedResult<ProductDto>> Handle(ProductGetAllQuery query, CancellationToken ct)
    {
        var items = await _repository.GetAllAsync(query, ct);
        var total = await _repository.GetTotalAsync(query, ct);
        return new PaginatedResult<ProductDto>(items, total, query.Page, query.PageSize);
    }
}