using BuildingBlocks.Domain;

namespace Shopping.Domain.Wishes;

public sealed record WishId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static WishId CreateUnique() => new WishId(Guid.NewGuid());

    public static WishId Create(Guid id) => new WishId(id);

    private WishId(Guid value)
    {
        Value = value;
    }

    private WishId()
    {
    }
}
