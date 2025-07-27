using CheckoutModule.Domain.Carts.Aggregates;

namespace OrderModule.Application.Services;

public class OrderFactory : IOrderFactory
{
    public Order CreateFromCart(Cart cart, Guid customerId, ShippingAddress address)
    {
        var orderItems = cart.Items.Select(item =>
            new OrderItem(
                productId: item.ProductId,
                productName: item.ProductName,
                quantity: item.Quantity,
                unitPrice: item.UnitPrice
            )
        ).ToList();
 

        var order = Order.Place(
            customerId: customerId,
            items: orderItems,
            address: address
        );

        return order;
    }
}