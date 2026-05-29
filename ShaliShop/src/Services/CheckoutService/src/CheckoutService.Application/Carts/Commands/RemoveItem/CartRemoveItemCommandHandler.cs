using CheckoutService.Application.Carts.Errors;
using CheckoutService.Domain.Carts.Repository;

namespace CheckoutService.Application.Carts.Commands.RemoveItem;

public class CartRemoveItemCommandHandler(
    ICartRepository carts,
    ICheckoutUnitOfWork uow
) : IRequestHandler<CartRemoveItemCommand, Result>
{
    public async Task<Result> Handle(CartRemoveItemCommand command, CancellationToken ct)
    {
        var cart = await carts.LoadAsync(command.CartId, ct);
        if (cart is null)
            return Result.Failure(new CartNotFoundError());
        if (cart.IsEmpty)
            return Result.Failure(new CartEmptyError());

        cart.RemoveItem(command.ProductId);
        await carts.SaveAsync(cart, ct);
        await uow.CommitAsync(ct);
        return Result.Success();
    }
}