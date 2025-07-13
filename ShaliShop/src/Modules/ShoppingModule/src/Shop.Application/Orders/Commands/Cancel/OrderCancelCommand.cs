namespace Shop.Application.Orders.Commands.Cancel;

public record OrderCancelCommand(Guid OrderId, string Reason) : IRequest<Result>;