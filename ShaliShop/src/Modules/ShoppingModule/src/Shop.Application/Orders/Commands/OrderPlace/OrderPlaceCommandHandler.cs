 
using Shared.Domain;
using SharedModule.Domain.ValueObjects; 

namespace Shop.Application.Orders.Commands.OrderPlace;

public class OrderPlaceCommandHandler
    : IRequestHandler<OrderPlaceCommand, Result<OrderPlacementResult>>
{
    public async Task<Result<OrderPlacementResult>> Handle(OrderPlaceCommand request, CancellationToken ct)
    {
        throw new BusinessRuleValidationException("Boom"); // Force test
    }
}
//
// public class OrderPlaceCommandHandler(
//     ICustomerRepository customers,
//     ICartRepository carts,
//     IOrderRepository orders,
//     IInventoryService inventory
//     ) : IRequestHandler<OrderPlaceCommand, Result<OrderPlacementResult>>
// {
//    
//      public async Task<CommandResult> Handle(OrderPlaceCommand command, CancellationToken ct)
//      { 
//          try
//          {
//              // 1. Load customer and cart
//              var customer = await customers.LoadAsync(command.CustomerId, ct);
//              var cart = await carts.LoadForCustomerAsync(command.CustomerId, ct);
//         
//              if (cart.IsEmpty)
//                  return CommandResult.Failure(new CartEmptyError());
//              // 2. Reserve stock
//              foreach (var item in cart.Items)
//              {
//                  var reserve = await inventory.TryReserveStockAsync(item.ProductId, item.Quantity, ct);
//                  if (!reserve.IsSuccess)
//                      return CommandResult.Failure(new StockUnavailableError(item.ProductName));
//              }
//         
//              // 3. Create order and save 
//              var address = command.ShippingAddress.Adapt<ShippingAddress>();
//              var order = Order.CreateFromCart(cart, customer.Id, address);
//              await orders.SaveAsync(order, ct);
//         
//              // 4. Optionally clear the cart
//              cart.Clear();
//              await carts.SaveAsync(cart, ct);
//         
//              return CommandResult.Success(new OrderPlacementResult(order.Id, order.TotalAmount));
//          }
//          catch (BusinessRuleValidationException ex)
//          {
//              return CommandResult.Failure(DomainError.Make(ex.Message));
//          }
//     }
// }