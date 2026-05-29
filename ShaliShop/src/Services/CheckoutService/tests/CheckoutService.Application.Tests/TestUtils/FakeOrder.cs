using CheckoutService.Application.Models;
using CheckoutService.Domain.Carts.Aggregates;
using OrderService.Domain.Orders.Aggregates;
using OrderService.Domain.Orders.ValueObjects;

namespace CheckoutService.Application.Tests.TestUtils;

public static class FakeOrder
{
    public static Order FromCart(Cart cart, ShippingAddressDto shippingDto)
    {
        var orderItems = cart.Items.Select(p => new OrderItem(p.ProductId, p.ProductName, p.Quantity, p.UnitPrice)).ToList();
        var order = Order.Place(cart.CustomerId, orderItems, new ShippingAddress(shippingDto.City, shippingDto.Street, shippingDto.ZipCode, shippingDto.State, shippingDto.Country));
        return order;
    }
}