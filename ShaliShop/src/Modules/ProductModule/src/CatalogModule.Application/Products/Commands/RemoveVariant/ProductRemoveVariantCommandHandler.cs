using CatalogModule.Application.Errors;
using CatalogModule.Domain.Products.Repository;

namespace CatalogModule.Application.Products.Commands.RemoveVariant;

public class ProductRemoveVariantCommandHandler(
    IProductRepository products,
    ICatalogUnitOfWork unitOfWork
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