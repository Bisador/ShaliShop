using CatalogModule.Application.Abstraction.Products;

namespace CatalogModule.Application.Products.Queries.GetAll;

public class ProductGetAllQuery : StandardQuery, IQuery<ProductDto>;