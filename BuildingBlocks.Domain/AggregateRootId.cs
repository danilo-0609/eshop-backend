using System;

namespace BuildingBlocks.Domain;

public abstract record AggregateRootId<TId> : EntityId<TId>
    where TId : notnull
{
    protected AggregateRootId(TId value)
    {
        Value = value;
    }

    protected AggregateRootId(EntityId<TId> original) : base(original)
    {
    }

    protected AggregateRootId()
    {
    }
}
