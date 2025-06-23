using Shop.Domain.Customers.DomainEvents;
using Shop.Domain.Customers.Rules;

namespace Shop.Domain.Customers.Aggregate;

public sealed class CustomerAggregate : AggregateRoot<Guid>
{
    public string FullName { get; private set; }
    public string Email { get; private set; }
    public bool IsActive { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    private CustomerAggregate()  
    {
    }

    private CustomerAggregate(string fullName, string email):base(Guid.NewGuid())
    { 
        FullName = fullName;
        Email = email;
        IsActive = true;
        RegisteredAt = DateTime.UtcNow;

        AddDomainEvent(new CustomerRegistered(Id, FullName, Email));
    }

    public static CustomerAggregate Register(string fullName, string email)
    {
        CheckRule(new FullNameIsRequired(fullName)); 
        return new CustomerAggregate(fullName, email);
    }

    public void UpdateProfile(string newName, string newEmail)
    {
        FullName = newName;
        Email = newEmail;

        AddDomainEvent(new CustomerUpdatedProfile(Id));
    }

    public void Deactivate()
    {
        IsActive = false;
        AddDomainEvent(new CustomerDeactivated(Id));
    }
}