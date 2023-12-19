using System;
using BuildingBlocks.Domain;

namespace Catalog.Domain.Products;

public sealed record ProductId : AggregateRootId<Guid>
{
    public override Guid Value { get; protected set; }

    private ProductId(Guid value)
        : base(value)
    {
        Value = value;
    }

    private ProductId()
    {
        
    }

    public static ProductId CreateUnique() => new ProductId(Guid.NewGuid());

    public static ProductId Create(Guid id)
    {
        return new ProductId(id);
    }
}

