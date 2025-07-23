namespace OrderModule.Application.Orders.Commands.OrderPay;
 

public record OrderPayCommand(Guid OrderId, PaymentDto Payment) : IRequest<Result>;
