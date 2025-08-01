namespace Shared.Domain;

public abstract class AggregateRoot : Entity<Guid>, IAuditableEntity
{
    protected AggregateRoot() : base()
    {
    }

    public AggregateRoot(Guid id) : base(id)
    {
    }

    #region AuditableEntity

    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }

    public string? CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }

    #endregion

    #region DomainEvent

    private readonly List<IDomainEvent> _domainEvents = [];
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void AddDomainEvent(IDomainEvent domainEvent) => _domainEvents.Add(domainEvent);

    public void ClearDomainEvents() => _domainEvents.Clear();

    #endregion

    protected static void CheckRule(DomainException rule)
    {
        if (rule.IsBroken())
            throw rule;
    }
}