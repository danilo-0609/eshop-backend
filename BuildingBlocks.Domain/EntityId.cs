using System;

namespace BuildingBlocks.Domain;

public abstract record EntityId<TId> : ValueObject
    where TId : notnull
{
    public abstract TId Value { get; protected set; }

    protected EntityId(TId value)
    {
        Value = value;
    }

    protected EntityId() { }
}
