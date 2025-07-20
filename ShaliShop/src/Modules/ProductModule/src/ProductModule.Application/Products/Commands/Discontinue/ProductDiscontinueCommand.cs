namespace ProductModule.Application.Products.Commands.Discontinue;

public record ProductDiscontinueCommand(
    Guid ProductId
) : ICommand;