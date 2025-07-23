namespace OrderModule.Application.Orders.Commands.Return;

public record ReturnedItemDto(
    Guid ProductId,
    decimal Quantity
);