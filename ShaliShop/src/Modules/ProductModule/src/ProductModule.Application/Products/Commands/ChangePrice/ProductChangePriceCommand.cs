namespace ProductModule.Application.Products.Commands.ChangePrice;

public record ProductChangePriceCommand(
    Guid ProductId,
    Money NewPrice
) : ICommand;