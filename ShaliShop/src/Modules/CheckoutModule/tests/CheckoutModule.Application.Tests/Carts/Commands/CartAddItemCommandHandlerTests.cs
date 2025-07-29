using CheckoutModule.Application.Carts.Commands.AddItem;
using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Application.Tests.TestUtils;

namespace CheckoutModule.Application.Tests.Carts.Commands;

public class CartAddItemCommandHandlerTests
{
    [Fact]
    public async Task Should_AddItem_And_Commit_When_CartExists()
    {
        var cart = new FakeCartBuilder().WithDefaultItems().Build();
        var repo = FakeCartRepository.Make(cart);
        var handler = new CartAddItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);

        var command = new CartAddItemCommand(cart.Id, Guid.NewGuid(), "Keyboard", 2, 89.99m);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        cart.Items.Should().ContainSingle(i => i.ProductName == "Keyboard");
    }

    [Fact]
    public async Task Should_ReturnFailure_When_CartNotFound()
    {
        var repo = FakeCartRepository.Make(); // No carts
        var handler = new CartAddItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);

        var command = new CartAddItemCommand(Guid.NewGuid(), Guid.NewGuid(), "Monitor", 1, 199.99m);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartNotFoundError>();
    }
}