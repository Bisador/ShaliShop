using CheckoutModule.Application.Carts.Commands.Clear;
using CheckoutModule.Application.Carts.Errors;
using CheckoutModule.Application.Tests.TestUtils;

namespace CheckoutModule.Application.Tests.Carts.Commands;

public class CartClearCommandHandlerTests
{
    [Fact]
    public async Task Should_ClearCart_And_Commit()
    {
        var cart = new FakeCartBuilder().WithDefaultItems(3).Build();    

        var repo = FakeCartRepository.Make(cart); 
        var handler = new CartClearCommandHandler(repo.Object, FakeCheckoutUnitOfWork.Make().Object);  

        var command = new CartClearCommand(cart.Id);
        var result = await handler.Handle(command, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        cart.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task Should_Fail_When_CartNotFound()
    {
        var handler = new CartClearCommandHandler(FakeCartRepository.Make().Object, FakeCheckoutUnitOfWork.Make().Object);   
        var result = await handler.Handle(new CartClearCommand(Guid.NewGuid()), CancellationToken.None);

        result.IsFailure.Should().BeTrue();
        result.Error.Should().BeOfType<CartNotFoundError>();
    }
}