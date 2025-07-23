namespace OrderModule.Application.Orders.Commands.Return;

public record OrderReturnCommand(
    Guid OrderId,
    List<ReturnedItemDto> Items
) : IRequest<Result>;