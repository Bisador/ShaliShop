namespace Shop.Application.Orders.Commands.Return;

public record ReturnedItemDto(
    Guid ProductId,
    int Quantity
);