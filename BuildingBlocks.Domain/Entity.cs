using System;
using ErrorOr;
using MediatR;

namespace BuildingBlocks.Domain;

public abstract class Entity<TId, TIdType> : IEquatable<Entity<TId, TIdType>>, IHasDomainEvents, IEntity
    where TId : EntityId<TIdType>
    where TIdType : notnull
{
    private readonly List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    protected ErrorOr<Unit> CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken())
        {
            return rule.Error;
        }

        return Unit.Value;
    }
    protected Entity() { }

    public override bool Equals(object? obj)
    {
        return obj is Entity<TId, TIdType> entity && Id.Equals(entity.Id);
    }

    public static bool operator ==(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(Entity<TId, TIdType> left, Entity<TId, TIdType> right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    protected void Raise(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvent() => _domainEvents?.Clear();

    public bool Equals(Entity<TId, TIdType>? other)
    {
        return Equals((object?)other);
    }
} 

