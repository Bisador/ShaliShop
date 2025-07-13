namespace Shop.Application.Orders.Commands.Ship;

public record OrderShipCommand(Guid OrderId) : IRequest<Result>;