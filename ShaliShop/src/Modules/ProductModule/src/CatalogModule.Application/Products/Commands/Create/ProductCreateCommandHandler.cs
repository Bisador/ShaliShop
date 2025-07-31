using CatalogModule.Domain.Products.Aggregates;
using CatalogModule.Domain.Products.Repository;

namespace CatalogModule.Application.Products.Commands.Create;

public class ProductCreateCommandHandler(
    IProductRepository products,
    ICatalogUnitOfWork unitOfWork
) : IRequestHandler<ProductCreateCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(ProductCreateCommand command, CancellationToken ct)
    {
        var product = Product.Create(command.Name, command.Description, command.Price, command.Category);

        await products.SaveAsync(product, ct);
        await unitOfWork.CommitAsync(ct);

        return Result<Guid>.Success(product.Id);
    }
}