using CheckoutModule.Domain.Carts.Aggregates;
using OrderModule.Domain.Orders.Aggregates;
using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Application.Abstraction;

public interface IOrderFactory
{
    Order CreateFromCart(Cart cart, Guid customerId, ShippingAddress address);
}