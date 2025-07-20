using ProductModule.Application.Errors;
using ProductModule.Domain.Products.Aggregates;
using ProductModule.Domain.Products.Repository;

namespace ProductModule.Application.Products.Commands.AddVariant;

public class ProductAddVariantCommandHandler(
    IProductRepository products,
    IProductUnitOfWork unitOfWork
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