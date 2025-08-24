using CatalogModule.Application.Errors;
using CatalogModule.Domain.Products.Repository;

namespace CatalogModule.Application.Products.Commands.Publish;

public class ProductPublishCommandHandler(
    IProductRepository products,
    ICatalogUnitOfWork unitOfWork
) : IRequestHandler<ProductPublishCommand, Result>
{
    public async Task<Result> Handle(ProductPublishCommand command, CancellationToken ct)
    {
        var product = await products.LoadAsync(command.ProductId, ct);
        if (product is null)
            return Result.Failure(new ProductNotFoundError(command.ProductId));

        product.Publish();

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}