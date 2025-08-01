namespace Shared.Domain;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    Guid AggregateId { get; }
}