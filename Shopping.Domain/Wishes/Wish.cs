using BuildingBlocks.Domain;
using Shopping.Domain.Items;

namespace Shopping.Domain.Wishes;

public sealed class Wish : AggregateRoot<WishId, Guid>
{
    private readonly List<ItemId> _itemsId = new(); 

    public new WishId Id { get; private set; }

    public Guid CustomerId {  get; private set; }

    public List<ItemId> Items { get; private set; }

    public string Name { get; private set; }
    
    public bool IsPrivate { get; private set; }

    public DateTime CreatedOn { get; private set; }

    private Wish(
        WishId id,
        Guid customerId,
        ItemId item,
        string name,
        bool isPrivate,
        DateTime createdOn)
    {
        Id = id;
        CustomerId = customerId;
        Name = name;
        IsPrivate = isPrivate;
        CreatedOn = createdOn;

        Items = _itemsId;
        _itemsId.Add(item);
    }

    public static Wish Create(
        Guid CustomerId,
        ItemId item,
        string name,
        bool isPrivate,
        DateTime createdOn)
    {
        return new Wish(
            WishId.CreateUnique(),
            CustomerId,
            item,
            name,
            isPrivate,
            createdOn);
    }

    public Wish AddItem(ItemId itemId)
    {
        _itemsId.Add(itemId); 
        
        return this;
    }

    public void RemoveItem(ItemId itemId)
    {
        _itemsId.Remove(itemId);
    }

    private Wish()
    {
    }
}
