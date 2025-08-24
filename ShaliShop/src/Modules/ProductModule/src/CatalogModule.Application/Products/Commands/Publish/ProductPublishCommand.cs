namespace CatalogModule.Application.Products.Commands.Publish;

public record ProductPublishCommand(
    Guid ProductId
) : ICommand;