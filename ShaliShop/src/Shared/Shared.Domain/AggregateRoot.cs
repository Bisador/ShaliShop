namespace Shared.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>
{
    protected AggregateRoot():base()
    {
    }

    public AggregateRoot(TId id) : base(id)
    {
    }
    
    private readonly List<IDomainEvent> _events = [];
    public IReadOnlyCollection<IDomainEvent> Events => _events.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _events.Add(domainEvent);

    public void ClearDomainEvents() => _events.Clear();
    
    protected static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
            throw new BusinessRuleValidationException(rule);
    }
}