using BuildingBlocks.Domain;

namespace UserAccess.Domain.UserRegistrations.Events;
public sealed record UserRegistrationConfirmedDomainEvent(
    Guid DomainEventId,
    UserRegistrationId Id,
    DateTime OcurredOn) : IDomainEvent;
