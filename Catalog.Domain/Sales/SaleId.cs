using BuildingBlocks.Domain;

namespace Catalog.Domain.Sales;
public sealed record SaleId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    public static SaleId Create(Guid value) => new SaleId(value);

    public static SaleId CreateUnique() => new SaleId(Guid.NewGuid());

    private SaleId(Guid value)
    {
        Value = value;
    }

    private SaleId () {}
}
