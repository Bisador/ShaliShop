using CatalogModule.Application.Abstraction.Products; 

namespace CatalogModule.Application.Products.Queries.GetById;

public class ProductGetByIdQueryHandler(IProductReadRepository repository) : IRequestHandler<ProductGetByIdQuery, ProductDto?>
{
    public async Task<ProductDto?> Handle(ProductGetByIdQuery query, CancellationToken ct)
    {
        var result = await repository.FindByIdAsync(query.ProductId, ct);
        return result;
    }
}