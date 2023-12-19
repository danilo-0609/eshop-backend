using BuildingBlocks.Domain;

namespace UserAccess.Domain.Users.Events;
public sealed record UserCreatedDomainEvent(
    Guid DomainEventId,
    UserId UserId,
    DateTime OcurredOn) : IDomainEvent;

   
