using ProductModule.Application.Errors;
using ProductModule.Domain.Products.Repository;

namespace ProductModule.Application.Products.Commands.ChangePrice;

public class ProductChangePriceCommandHandler(
    IProductRepository products,
    IProductUnitOfWork unitOfWork
) : IRequestHandler<ProductChangePriceCommand, Result>
{
    public async Task<Result> Handle(ProductChangePriceCommand command, CancellationToken ct)
    {
        var product = await products.LoadAsync(command.ProductId, ct);
        if (product is null)
            return Result.Failure(new ProductNotFoundError(command.ProductId));

        product.ChangePrice(command.NewPrice);

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result.Success();
    }
}