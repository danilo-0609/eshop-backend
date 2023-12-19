using BuildingBlocks.Domain;

namespace UserAccess.Domain.UserRegistrations.Events;
public record UserRegistrationExpiredDomainEvent(
    Guid DomainEventId,
    UserRegistrationId Id,
    DateTime OcurredOn) : IDomainEvent;



