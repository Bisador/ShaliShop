using CheckoutModule.Application.Carts.Commands.RemoveItem;
using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Application.Tests.TestUtils;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Tests.Carts.Commands;

public class CartRemoveItemCommandHandlerTests
{
    [Fact]
    public async Task Should_RemoveItem_And_Commit()
    {
        var cart = new FakeCartBuilder().Build();
        cart.AddItem(Guid.NewGuid(), "Mouse", Money.From(49), 1);
        var repo = FakeCartRepository.Make(cart);
        var handler = new CartRemoveItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);

        var command = new CartRemoveItemCommand(cart.Id, cart.Items.First().ProductId);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Fail_When_CartIsEmpty()
    {
        var cart = new FakeCartBuilder().Build();
        var repo = FakeCartRepository.Make(cart);
        var handler = new CartRemoveItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);

        var command = new CartRemoveItemCommand(cart.Id, Guid.NewGuid());
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartEmptyError>();
    }

    [Fact]
    public async Task Should_Fail_When_CartNotFound()
    {
        var handler = new CartRemoveItemCommandHandler(FakeCartRepository.Make().Object, FakeCheckoutUnitOfWork.Make().Object);
        var command = new CartRemoveItemCommand(Guid.NewGuid(), Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartNotFoundError>();
    }
}