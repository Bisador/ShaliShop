namespace CheckoutModule.Application.Carts.Commands.RemoveItem;

public record CartRemoveItemCommand(Guid CartId, Guid ProductId)
    : ICommand;