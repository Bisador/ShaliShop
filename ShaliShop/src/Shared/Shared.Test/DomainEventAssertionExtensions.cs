using Shared.Domain;

namespace Shared.Test;

public static class DomainEventAssertionExtensions
{
    public static T ShouldHaveDomainEvent<T>(this AggregateRoot<Guid> aggregate)
        where T : class, IDomainEvent
    {
        var domainEvent = aggregate.Events.OfType<T>().FirstOrDefault();

        domainEvent.Should().NotBeNull($"Expected domain event of type {typeof(T).Name} was not raised.");

        return domainEvent!;
    }
}
