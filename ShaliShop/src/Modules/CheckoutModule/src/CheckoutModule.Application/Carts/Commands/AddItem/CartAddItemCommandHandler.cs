using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Domain.Carts.Repository;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Carts.Commands.AddItem;

public class CartAddItemCommandHandler(
    ICartRepository carts,
    ICheckoutUnitOfWork uow
) : IRequestHandler<CartAddItemCommand, Result>
{
    public async Task<Result> Handle(CartAddItemCommand command, CancellationToken ct)
    {
        var cart = await carts.LoadAsync(command.CartId, ct);
        if (cart is null)
            return Result.Failure(new CartNotFoundError()); 

        cart.AddItem(command.ProductId, command.ProductName, Money.From(command.UnitPrice), command.Quantity);
        await carts.SaveAsync(cart, ct);
        await uow.CommitAsync(ct);
        return Result.Success();
    }
}