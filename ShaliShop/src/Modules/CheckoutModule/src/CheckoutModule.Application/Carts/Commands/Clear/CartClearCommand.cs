namespace CheckoutModule.Application.Carts.Commands.Clear;

public record CartClearCommand(Guid CartId)
    : ICommand;