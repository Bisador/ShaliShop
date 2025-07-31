using CatalogModule.Application.Abstraction.Products;

namespace CatalogModule.Application.Products.Queries.GetById;

public record ProductGetByIdQuery(Guid ProductId) : IRequest<ProductDto?>;