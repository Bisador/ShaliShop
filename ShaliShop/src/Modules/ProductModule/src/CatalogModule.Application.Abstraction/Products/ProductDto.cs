namespace CatalogModule.Application.Abstraction.Products;

public record ProductDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    bool IsPublished,
    bool IsDiscontinued);