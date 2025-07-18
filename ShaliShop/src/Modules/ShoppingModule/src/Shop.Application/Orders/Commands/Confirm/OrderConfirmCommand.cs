namespace Shop.Application.Orders.Commands.Confirm;

public record OrderConfirmCommand(Guid OrderId) : IRequest<Result>;