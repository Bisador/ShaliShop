namespace Shared.Domain;

public abstract record DomainEvent(Guid AggregateId) : IDomainEvent
{
    public DateTime OccurredOn { get; } = DateTime.UtcNow;
}