using ProductModule.Application.Errors;
using ProductModule.Domain.Products.Repository;

namespace ProductModule.Application.Products.Commands.Discontinue;

public class ProductDiscontinueCommandHandler(
    IProductRepository products,
    IProductUnitOfWork unitOfWork
) : IRequestHandler<ProductDiscontinueCommand, Result>
{
    public async Task<Result> Handle(ProductDiscontinueCommand command, CancellationToken ct)
    {
        var product = await products.LoadAsync(command.ProductId, ct);
        if (product is null)
            return Result.Failure(new ProductNotFoundError(command.ProductId));

        product.Discontinue();

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}