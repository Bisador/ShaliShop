namespace CheckoutModule.Application.Abstraction;

public interface ICheckoutUnitOfWork
{
    Task CommitAsync(CancellationToken ct);
}