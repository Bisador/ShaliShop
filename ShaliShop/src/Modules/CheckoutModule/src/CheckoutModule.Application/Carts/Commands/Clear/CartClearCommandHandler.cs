using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Domain.Carts.Repository;

namespace CheckoutModule.Application.Carts.Commands.Clear;

public class CartClearCommandHandler(
    ICartRepository carts,
    ICheckoutUnitOfWork uow
) : IRequestHandler<CartClearCommand, Result>
{
    public async Task<Result> Handle(CartClearCommand command, CancellationToken ct)
    {
        var cart = await carts.LoadAsync(command.CartId, ct);
        if (cart is null)
            return Result.Failure(new CartNotFoundError()); 

        cart.Clear();
        await carts.SaveAsync(cart, ct);
        await uow.CommitAsync(ct);
        return Result.Success();
    }
}