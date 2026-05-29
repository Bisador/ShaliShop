namespace CatalogService.Application.Products.Queries.GetById;

public record ProductGetByIdQuery(Guid ProductId) : IRequest<ProductDto?>;