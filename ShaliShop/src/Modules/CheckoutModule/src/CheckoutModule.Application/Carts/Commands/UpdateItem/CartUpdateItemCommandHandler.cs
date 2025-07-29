using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Domain.Carts.Repository;

namespace CheckoutModule.Application.Carts.Commands.UpdateItem;

public class CartUpdateItemCommandHandler(
    ICartRepository carts,
    ICheckoutUnitOfWork uow
) : IRequestHandler<CartUpdateItemCommand, Result>
{
    public async Task<Result> Handle(CartUpdateItemCommand command, CancellationToken ct)
    {
        var cart = await carts.LoadAsync(command.CartId, ct);
        if (cart is null)
            return Result.Failure(new CartNotFoundError());
        if (cart.IsEmpty)
            return Result.Failure(new CartEmptyError());

        cart.UpdateItemQuantity(command.ProductId, command.NewQuantity);
        await carts.SaveAsync(cart, ct);
        await uow.CommitAsync(ct);
        return Result.Success();
    }
}