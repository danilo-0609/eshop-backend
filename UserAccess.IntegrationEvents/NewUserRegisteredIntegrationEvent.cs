using BuildingBlocks.Application.IntegrationEvents;

namespace UserAccess.IntegrationEvents;

public sealed record NewUserRegisteredIntegrationEvent(
    Guid IntegrationEventId,
    Guid UserRegistrationId,
    string Login,
    string Email,
    string FirstName,
    string LastName,
    string Name,
    DateTime RegisteredDate) : IntegrationEvent(IntegrationEventId, RegisteredDate);
