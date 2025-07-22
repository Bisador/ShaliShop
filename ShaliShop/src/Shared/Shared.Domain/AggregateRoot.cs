namespace Shared.Domain;

public abstract class AggregateRoot<TId> : Entity<TId>, IAuditableEntity
{
    protected AggregateRoot() : base()
    {
    }

    public AggregateRoot(TId id) : base(id)
    {
    }

    #region AuditableEntity

    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }

    #endregion

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

 