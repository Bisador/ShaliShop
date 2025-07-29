namespace CheckoutModule.Application.Carts.Commands.UpdateItem;

public record CartUpdateItemCommand(Guid CartId, Guid ProductId, decimal NewQuantity)
    : ICommand;