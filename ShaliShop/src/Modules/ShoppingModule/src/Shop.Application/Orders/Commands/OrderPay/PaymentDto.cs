using Shop.Domain.Orders.Enums;

namespace Shop.Application.Orders.Commands.OrderPay;
 
public record PaymentDto(
    string TransactionId,
    string Method,        
    DateTime PaidAt
);
