namespace CatalogModule.Application.Products.Commands.AddVariant;

public record ProductAddVariantCommand(
    Guid ProductId,
    string Sku,
    Dictionary<string, string> Options,
    Money? PriceOverride
) : ICommand;