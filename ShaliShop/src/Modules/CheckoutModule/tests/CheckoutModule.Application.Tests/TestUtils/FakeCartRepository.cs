using CheckoutModule.Application.Abstraction;
using CheckoutModule.Domain.Carts.Aggregates;
using CheckoutModule.Domain.Carts.Repository;

namespace CheckoutModule.Application.Tests.TestUtils;

public static class FakeCartRepository
{
    public static Mock<ICartRepository> Make(Cart cart)
    {
        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(cart.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(cart);
        return cartRepo;
    }

    public static Mock<ICartRepository> Make()
    {
        var cartRepo = new Mock<ICartRepository>();
        cartRepo.Setup(r => r.LoadAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Cart?) null);
        return cartRepo;
    }
}

public static class FakeCheckoutUnitOfWork
{
    public static Mock<ICheckoutUnitOfWork> Make() => new();
}