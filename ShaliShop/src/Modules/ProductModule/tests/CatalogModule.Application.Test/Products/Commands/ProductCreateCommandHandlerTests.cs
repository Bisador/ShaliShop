using CatalogModule.Application.Products.Commands.Create;

namespace CatalogModule.Application.Test.Products.Commands;

public class ProductCreateCommandHandlerTests
{
    private readonly Mock<IProductRepository> _products = new();
    private readonly Mock<ICatalogUnitOfWork> _unitOfWork = new();
    private readonly ProductCreateCommandHandler _handler;

    public ProductCreateCommandHandlerTests()
    {
        _handler = new ProductCreateCommandHandler(_products.Object, _unitOfWork.Object);
    }
    [Fact]
    public async Task Should_create_product_and_return_id()
    {
        var command = new ProductCreateCommand(
            "Tablet",
            "Lightweight Android tablet",
            new Money(129.99m),
            "Electronics"
        );

        _products.Setup(r => r.SaveAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _unitOfWork.Setup(u => u.CommitAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var result = await _handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();

        _products.Verify(r => r.SaveAsync(It.Is<Product>(p =>
            p.Name == "Tablet" &&
            p.Description.Contains("Android") &&
            p.Category == "Electronics" &&
            p.Price.Amount == 129.99m
        ), It.IsAny<CancellationToken>()), Times.Once);

        _unitOfWork.Verify(u => u.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    [Fact]
    public async Task Should_raise_ProductCreated_event()
    {
        var command = new ProductCreateCommand("Camera", "DSLR 24MP", new Money(499.00m), "Photography");

        Product? captured = null;
        _products.Setup(r => r.SaveAsync(It.IsAny<Product>(), It.IsAny<CancellationToken>()))
            .Callback<Product, CancellationToken>((p, _) => captured = p)
            .Returns(Task.CompletedTask);

        await _handler.Handle(command, CancellationToken.None);

        captured.Should().NotBeNull();
        captured!.Events.Any(e =>
            e is ProductCreated created &&
            created.ProductId == captured.Id &&
            created is {Name: "Camera", Category: "Photography"}
        ).Should().BeTrue();
    }

}