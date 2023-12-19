using BuildingBlocks.Domain;

namespace Shopping.Domain.Basket;
public sealed record BasketId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static BasketId Create(Guid value) => new BasketId(value);

    public static BasketId CreateUnique() => new BasketId(Guid.NewGuid());

    private BasketId(Guid value)
    {
        Value = value;
    }
}