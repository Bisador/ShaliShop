using CatalogModule.Application.Errors;
using CatalogModule.Domain.Products.Aggregates;
using CatalogModule.Domain.Products.Repository;

namespace CatalogModule.Application.Products.Commands.AddVariant;

public class ProductAddVariantCommandHandler(
    IProductRepository products,
    ICatalogUnitOfWork unitOfWork
) : IRequestHandler<ProductAddVariantCommand, Result>
{
    public async Task<Result> Handle(ProductAddVariantCommand command, CancellationToken ct)
    {
        var product = await products.LoadAsync(command.ProductId, ct);
        if (product is null)
            return Result.Failure(new ProductNotFoundError(command.ProductId));

        var variant = new ProductVariant(
            command.Sku,
            command.Options,
            command.PriceOverride
        );

        product.AddVariant(variant);

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}