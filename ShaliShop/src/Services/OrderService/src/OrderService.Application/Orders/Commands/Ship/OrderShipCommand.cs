namespace OrderService.Application.Orders.Commands.Ship;

public record OrderShipCommand(Guid OrderId) : IRequest<Result>;