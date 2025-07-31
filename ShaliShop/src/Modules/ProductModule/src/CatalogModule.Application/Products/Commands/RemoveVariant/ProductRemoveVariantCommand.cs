namespace CatalogModule.Application.Products.Commands.RemoveVariant;

public record ProductRemoveVariantCommand(
    Guid ProductId,
    string Sku
) : ICommand;