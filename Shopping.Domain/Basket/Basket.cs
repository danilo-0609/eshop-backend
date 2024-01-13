using BuildingBlocks.Domain;
using Shopping.Domain.Basket.Events;
using Shopping.Domain.Items;

namespace Shopping.Domain.Basket;

public sealed class Basket : AggregateRoot<BasketId, Guid>
{
    private readonly List<ItemId> _itemIds = new List<ItemId>();

    public new BasketId Id { get; private set; }

    public Guid CustomerId { get; private set; }

    public IReadOnlyList<ItemId> ItemIds => _itemIds.AsReadOnly();

    public int AmountOfProducts => _itemIds.Count;

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

    public static Basket Create(
        Guid customerId,
        ItemId itemId,
        decimal totalAmount,
        DateTime createdOn)
    {
        Basket basket = new Basket(
            BasketId.CreateUnique(),
            customerId,
            totalAmount,
            createdOn);

        basket._itemIds.Add(itemId);

        return basket;
    }

    public void AddItem(ItemId itemId)
    {
        _itemIds.Add(itemId);
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



    private Basket(){}
}