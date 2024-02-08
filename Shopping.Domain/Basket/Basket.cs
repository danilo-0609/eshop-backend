using BuildingBlocks.Domain;
using Shopping.Domain.Basket.Events;
using Shopping.Domain.Items;

namespace Shopping.Domain.Basket;

public sealed class Basket : AggregateRoot<BasketId, Guid>
{
    private readonly List<ItemId> _itemIds = new List<ItemId>();

    private readonly Dictionary<Guid, int> _amountPerItem = new Dictionary<Guid, int>();

    public new BasketId Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public IReadOnlyList<ItemId> ItemIds => _itemIds.AsReadOnly();

    public IReadOnlyDictionary<Guid, int> AmountPerItem => _amountPerItem.AsReadOnly();

    public int AmountOfProducts { get; set; }

    public decimal TotalAmount { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    private Basket(
        BasketId id,
        Guid customerId,
        decimal totalAmount,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    private Basket(
     BasketId id,
     Guid customerId,
     decimal totalAmount,
     DateTime createdOn,
     int amountOfProducts,
     DateTime? updatedOn = null)
    {
        Id = id;
        CustomerId = customerId;
        TotalAmount = totalAmount;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;

        AmountOfProducts = amountOfProducts;
    }

    public static Basket Create(
        Guid customerId,
        ItemId itemId,
        decimal totalAmount,
        int amountOfProducts,
        DateTime createdOn)
    {
        Basket basket = new Basket(
            BasketId.CreateUnique(),
            customerId,
            totalAmount,
            createdOn);

        basket.AddItem(itemId, amountOfProducts);

        return basket;
    }

    public Basket Update(DateTime updatedOn, ItemId itemId, int actualAmountOfProducts, int amountRequested)
    {
        Basket basket = new Basket(
            Id,
            CustomerId,
            TotalAmount,
            CreatedOn,
            actualAmountOfProducts,
            updatedOn);

        basket.AddItem(itemId, amountRequested);

        return basket;
    }

    public void AddItem(ItemId itemId, int amount)
    {
        _itemIds.Add(itemId);

        _amountPerItem.Add(itemId.Value, amount);

        AmountOfProducts = AmountOfProducts + amount;
    }

    public void RemoveItem(ItemId itemId)
    {
        _itemIds.Remove(itemId);
    }

    public void Buy()
    {
        Raise(new BasketBuyRequestedDomainEvent(
            Guid.NewGuid(),
            Id,
            CustomerId,
            ItemIds,
            TotalAmount,
            DateTime.UtcNow));
    }

    public void ClearBasket()
    {
        _itemIds.Clear();
    }

    private Basket(){}
}