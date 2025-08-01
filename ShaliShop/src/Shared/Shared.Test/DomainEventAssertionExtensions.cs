using Shared.Domain;

namespace Shared.Test;

public static class DomainEventAssertionExtensions
{
    public static T ShouldHaveDomainEvent<T>(this AggregateRoot aggregate)
        where T : class, IDomainEvent
    {
        var domainEvent = aggregate.DomainEvents.OfType<T>().FirstOrDefault();

        domainEvent.Should().NotBeNull($"Expected domain event of type {typeof(T).Name} was not raised.");

        return domainEvent;
    }
}