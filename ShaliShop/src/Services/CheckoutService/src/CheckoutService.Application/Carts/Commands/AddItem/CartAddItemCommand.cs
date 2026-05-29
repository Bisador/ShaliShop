namespace CheckoutService.Application.Carts.Commands.AddItem;

public record CartAddItemCommand(Guid CartId, Guid ProductId, string ProductName, int Quantity, decimal UnitPrice)
    : ICommand;