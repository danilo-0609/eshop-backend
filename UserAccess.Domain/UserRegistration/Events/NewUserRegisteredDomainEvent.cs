using BuildingBlocks.Domain;
using MediatR;

namespace UserAccess.Domain.UserRegistrations.Events;

public sealed record NewUserRegisteredDomainEvent(
    Guid DomainEventId,
    UserRegistrationId Id,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    DateTime OcurredOn) : IDomainEvent, INotification;