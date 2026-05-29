using CheckoutService.Domain.Carts.Aggregates;
using OrderService.Domain.Orders.Aggregates;
using OrderService.Domain.Orders.ValueObjects;

namespace OrderService.Application.Abstraction;

public interface IOrderFactory
{
    Order CreateFromCart(Cart cart, Guid customerId, ShippingAddress address);
}