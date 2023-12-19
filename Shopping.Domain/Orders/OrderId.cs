using BuildingBlocks.Domain;

namespace Shopping.Domain.Orders;

public sealed record OrderId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static OrderId Create(Guid value) => new OrderId(value);

    public static OrderId CreateUnique() => new OrderId(Guid.NewGuid());

    private OrderId(Guid value)
    {
        Value = value;
    }
}