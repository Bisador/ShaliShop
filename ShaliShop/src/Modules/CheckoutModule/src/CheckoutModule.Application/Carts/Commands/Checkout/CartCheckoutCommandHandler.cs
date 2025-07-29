using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Domain.Carts.Repository;
using InventoryModule.Domain.Inventories.Repository;
using OrderModule.Application.Abstraction;
using OrderModule.Domain.Orders.DomainEvents;
using OrderModule.Domain.Orders.Repository;
using OrderModule.Domain.Orders.ValueObjects;

namespace CheckoutModule.Application.Carts.Commands.Checkout;

public class CartCheckoutCommandHandler(
    ICartRepository carts,
    IInventoryRepository inventory,
    IOrderFactory orderFactory,
    IOrderRepository orders,
    IIntegrationEventPublisher eventPublisher,
    ICheckoutUnitOfWork checkoutUnitOfWork,
    IOrderUnitOfWork orderUnitOfWork
) : IRequestHandler<CartCheckoutCommand, Result<CartCheckoutResult>>
{
    public async Task<Result<CartCheckoutResult>> Handle(CartCheckoutCommand command, CancellationToken ct)
    {
        // 1. Load Cart
        var cart = await carts.LoadAsync(command.CartId, ct);
        if (cart is null || cart.IsEmpty)
            return Result<CartCheckoutResult>.Failure(new CartEmptyError());

        // 2. Reserve Inventory
        foreach (var item in cart.Items)
        {
            var reserve = await inventory.TryReserveStockAsync(item.ProductId, item.Quantity, ct);
            if (!reserve.IsSuccess)
                return Result<CartCheckoutResult>.Failure(new StockUnavailableError(item.ProductName));
        }

        // 3. Create Order
        var dto = command.ShippingAddress;
        var address = new ShippingAddress(dto.City, dto.Street, dto.ZipCode, dto.State, dto.Country);
        var order = orderFactory.CreateFromCart(cart, cart.CustomerId, address);
        await orders.SaveAsync(order, ct);

        // 4. Clear Cart
        cart.Clear();
        await carts.SaveAsync(cart, ct);

        // 5. Publish Integration Event
        var @event = new OrderPlaced(order.Id, order.CustomerId, order.Items, order.TotalAmount);
        await eventPublisher.PublishAsync(@event, ct);

        await UnitOfWorkCoordinator.CommitAllAsync(
            () => checkoutUnitOfWork.CommitAsync(ct),
            () => orderUnitOfWork.CommitAsync(ct));

        await checkoutUnitOfWork.CommitAsync(ct);

        return Result<CartCheckoutResult>.Success(new CartCheckoutResult(order.Id, order.TotalAmount));
    }
}