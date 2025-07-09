namespace Shop.Application.Orders.Commands.OrderPay;
 

public record OrderPayCommand(Guid OrderId, PaymentDto Payment) : IRequest<Result>;
