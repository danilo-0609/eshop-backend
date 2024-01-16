using BuildingBlocks.Domain;

namespace Shopping.Domain.Items;

public sealed class Item : AggregateRoot<ItemId, Guid>
{
    public new ItemId Id { get; private set; }

    public string Name { get; private set; }

    public Guid SellerId { get; private set; }

    public decimal Price { get; private set; }

    public int InStock { get; private set; }

    public StockStatus StockStatus { get; private set; }

    public DateTime CreatedOn { get; private set; }

    public DateTime? UpdatedOn { get; private set; }

    private Item(
        ItemId id,
        string name,
        Guid sellerId,
        decimal price,
        int inStock,
        StockStatus stockStatus,
        DateTime createdOn,
        DateTime? updatedOn = null)
    {
        Id = id;
        Name = name;
        SellerId = sellerId;
        Price = price;
        InStock = inStock;
        StockStatus = stockStatus;
        CreatedOn = createdOn;
        UpdatedOn = updatedOn;
    }

    public static Item Create(
        Guid id,
        string name,
        Guid sellerId,
        decimal price,
        int inStock, 
        DateTime createdOn)
    {
        var item = new Item(
            ItemId.Create(id),
            name,
            sellerId,
            price,
            inStock,
            StockStatus.WithStock,
            createdOn);

        return item;
    }

    public static Item Update(
        Guid id,
        string name,
        Guid sellerId,
        decimal price,
        int inStock,
        DateTime createdOn,
        DateTime updatedOn)
    {
        var item = new Item(
            ItemId.Create(id),
            name,
            sellerId,
            price,
            inStock,
            StockStatus.WithStock,
            createdOn,
            updatedOn);

        return item;
    }

    public void OutOfStock()
    {
        StockStatus = StockStatus.OutOfStock;
    }

    private Item(){}
}