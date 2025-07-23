using OrderModule.Domain.Customers.DomainEvents;
using OrderModule.Domain.Customers.Rules;

namespace OrderModule.Domain.Customers.Aggregate;

public sealed class Customer : AggregateRoot<Guid>
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public bool IsActive { get; private set; }
    public DateTime RegisteredAt { get; private set; }

    private Customer()  
    {
    }

    private Customer(string fullName, string email):base(Guid.NewGuid())
    { 
        FullName = fullName;
        Email = email;
        IsActive = true;
        RegisteredAt = DateTime.UtcNow;

        AddDomainEvent(new CustomerRegistered(Id, FullName, Email));
    }

    public static Customer Register(string fullName, string email)
    {
        CheckRule(new FullNameIsRequired(fullName)); 
        return new Customer(fullName, email);
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