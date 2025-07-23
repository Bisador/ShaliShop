using CheckoutModule.Domain.Carts.DomainEvents;
using CheckoutModule.Domain.Carts.Rules;
using CheckoutModule.Domain.Carts.ValueObjects;

namespace CheckoutModule.Domain.Carts.Aggregates;

public sealed class Cart : AggregateRoot<Guid>
{
    public Guid CustomerId { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime LastModified { get; private set; }

    private readonly List<CartItem> _items = new();
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    private Cart()
    {
    } // For EF

    private Cart(Guid customerId) : base(Guid.NewGuid())
    {
        CustomerId = customerId;
        CreatedAt = LastModified = DateTime.UtcNow;

        AddDomainEvent(new CartCreated(Id, customerId));
    }

    public static Cart Create(Guid customerId) => new(customerId);

    public void AddItem(Guid productId, string productName, Money unitPrice, int quantity)
    {
        CheckRule(new QuantityMustBeGreaterThanZero(quantity));

        var existing = _items.FirstOrDefault(i => i.ProductId == productId);

        if (existing is not null)
        {
            existing.IncreaseQuantity(quantity);
            AddDomainEvent(new ItemQuantityUpdated(Id, productId, existing.Quantity));
        }
        else
        {
            var item = new CartItem(productId, productName, quantity, unitPrice);
            _items.Add(item);
            AddDomainEvent(new ItemAddedToCart(Id, item));
        }

        LastModified = DateTime.UtcNow;
    }

    public void RemoveItem(Guid productId)
    {
        var index = _items.FindIndex(i => i.ProductId == productId);
        CheckRule(new ProductNotFound(index)); 
        _items.RemoveAt(index);
        AddDomainEvent(new ItemRemovedFromCart(Id, productId));
        LastModified = DateTime.UtcNow; }

    public void Clear()
    {
        _items.Clear();
        AddDomainEvent(new CartCleared(Id));
        LastModified = DateTime.UtcNow;}

    public bool IsEmpty => !_items.Any();
}