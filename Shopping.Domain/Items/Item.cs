using BuildingBlocks.Domain;

namespace Shopping.Domain.Items;
public sealed class Item : AggregateRoot<ItemId, Guid>
{
    public new ItemId Id { get; private set; }

    private Item(ItemId id)
    {
        Id = id;
    }

    private Item(){}
}