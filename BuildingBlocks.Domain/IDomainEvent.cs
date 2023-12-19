using MediatR;

namespace BuildingBlocks.Domain;
public interface IDomainEvent : INotification
{
    Guid DomainEventId { get; }  

    DateTime OcurredOn { get; }      
}

