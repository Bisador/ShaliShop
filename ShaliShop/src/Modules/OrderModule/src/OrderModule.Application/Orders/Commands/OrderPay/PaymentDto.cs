namespace OrderModule.Application.Orders.Commands.OrderPay;
 
public record PaymentDto(
    string TransactionId,
    string Method,        
    DateTime PaidAt
);
