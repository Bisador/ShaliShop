using CheckoutModule.Application.Carts.Commands.UpdateItem;
using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Application.Tests.TestUtils;
using SharedModule.Domain.ValueObjects;

namespace CheckoutModule.Application.Tests.Carts.Commands;

public class CartUpdateItemCommandHandlerTests
{
    [Fact]
    public async Task Should_UpdateItemQuantity_And_Commit()
    {
        var cart = new FakeCartBuilder().Build();
        var productId = Guid.NewGuid();
        cart.AddItem(productId, "SSD", Money.From(120), 1);

        var repo = FakeCartRepository.Make(cart);
        var handler = new CartUpdateItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);

        var command = new CartUpdateItemCommand(cart.Id, productId, 3);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        cart.Items.First().Quantity.Should().Be(3);
    }

    [Fact]
    public async Task Should_Fail_When_CartIsEmpty()
    {
        var cart = new FakeCartBuilder().Build(); 
        var repo = FakeCartRepository.Make(cart);
        var handler = new CartUpdateItemCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object); 

        var command = new CartUpdateItemCommand(cart.Id, Guid.NewGuid(), 2);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartEmptyError>();
    }
}