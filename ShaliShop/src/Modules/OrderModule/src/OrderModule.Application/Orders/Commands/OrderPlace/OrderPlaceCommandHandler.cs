using CheckoutModule.Domain.Carts.Repository;
using InventoryModule.Domain.Inventories.Repository;
using OrderModule.Application.Orders.Commands.Errors; 
using OrderModule.Domain.Customers.Repository;
using OrderModule.Domain.Orders.Aggregates;
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Repository;
using OrderModule.Domain.Orders.ValueObjects;

namespace OrderModule.Application.Orders.Commands.OrderPlace;

using CommandResult = Result<OrderPlacementResult>;

public class OrderPlaceCommandHandler(
    ICustomerRepository customers,
    ICartRepository carts,
    IOrderRepository orders,
    IInventoryRepository inventory,
    IIntegrationEventPublisher eventPublisher,
    IShoppingUnitOfWork unitOfWork
) : IRequestHandler<OrderPlaceCommand, CommandResult>
{
    public async Task<CommandResult> Handle(OrderPlaceCommand command, CancellationToken ct)
    {
        // 1. Load customer and cart
        var customer = await customers.LoadAsync(command.CustomerId, ct);
        var cart = await carts.LoadForCustomerAsync(command.CustomerId, ct);

        if (cart.IsEmpty)
            return CommandResult.Failure(new CartEmptyError());
        // 2. Reserve stock
        foreach (var item in cart.Items)
        {
            var reserve = await inventory.TryReserveStockAsync(item.ProductId, item.Quantity, ct);
            if (!reserve.IsSuccess)
                return CommandResult.Failure(new StockUnavailableError(item.ProductName));
        }

        // 3. Create order and save
        var shippingAddressDto = command.ShippingAddress;
        var address = new ShippingAddress(shippingAddressDto.City, shippingAddressDto.Street, shippingAddressDto.ZipCode, shippingAddressDto.State, shippingAddressDto.Country);
        var order = Order.CreateFromCart(cart, customer.Id, address);
        await orders.SaveAsync(order, ct);

        // 4. Optionally clear the cart
        cart.Clear();
        await carts.SaveAsync(cart, ct);

        //5. publish the OrderPlacedEvent
        var @event = new OrderPlaced(
            order.Id,
            order.CustomerId,
            order.Items,
            order.TotalAmount
        );
        await eventPublisher.PublishAsync(@event, ct);

        await unitOfWork.CommitAsync(ct);

        return CommandResult.Success(new OrderPlacementResult(order.Id, order.TotalAmount));
    }
}