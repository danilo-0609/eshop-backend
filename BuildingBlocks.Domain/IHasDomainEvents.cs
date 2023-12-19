using System;

namespace BuildingBlocks.Domain;

public interface IHasDomainEvents
{
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    
    public void ClearDomainEvent();
}

