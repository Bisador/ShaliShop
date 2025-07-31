namespace CatalogModule.Application.Products.Commands.Create;

public record ProductCreateCommand(
    string Name,
    string Description,
    Money Price,
    string Category
) : ICommand<Guid>;