using ProductModule.Application.Errors;
using ProductModule.Domain.Products.Repository;

namespace ProductModule.Application.Products.Commands.RemoveVariant;

public class ProductRemoveVariantCommandHandler(
    IProductRepository products,
    IProductUnitOfWork unitOfWork
) : IRequestHandler<ProductRemoveVariantCommand, Result>
{
    public async Task<Result> Handle(ProductRemoveVariantCommand command, CancellationToken ct)
    {
        var product = await products.LoadAsync(command.ProductId, ct);
        if (product is null)
            return Result.Failure(new ProductNotFoundError(command.ProductId));

        product.RemoveVariant(command.Sku);

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}