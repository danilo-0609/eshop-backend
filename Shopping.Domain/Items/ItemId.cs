using BuildingBlocks.Domain;

namespace Shopping.Domain.Items;

public sealed record ItemId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static ItemId Create(Guid value) => new ItemId(value);

    private ItemId(Guid value)
    {
        Value = value;
    }
}