using System;

namespace BuildingBlocks.Domain;

public abstract class AggregateRoot<TId, TIdType> : Entity<TId, TIdType>, IAggregateRoot
    where TId : AggregateRootId<TIdType>
    where TIdType : notnull
{
    public new AggregateRootId<TIdType> Id { get; protected set; }

    protected AggregateRoot(TId id)
        : base(id)
    {
        Id = id;
    }

    protected AggregateRoot(){ }
}


