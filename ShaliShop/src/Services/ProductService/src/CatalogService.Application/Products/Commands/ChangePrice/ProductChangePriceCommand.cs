namespace CatalogService.Application.Products.Commands.ChangePrice;

public record ProductChangePriceCommand(
    Guid ProductId,
    Money NewPrice
) : ICommand;