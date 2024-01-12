using BuildingBlocks.Domain;

namespace Shopping.Domain.Buying;

public sealed record BuyId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static BuyId Create(Guid id) => new BuyId(id);

    public static BuyId CreateUnique() => new BuyId(Guid.NewGuid()); 

    public BuyId(Guid value)
    {
        Value = value;
    }
}