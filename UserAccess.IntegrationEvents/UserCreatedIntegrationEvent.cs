using BuildingBlocks.Application.IntegrationEvents;

namespace UserAccess.IntegrationEvents;

public sealed record UserCreatedIntegrationEvent(
    Guid IntegrationEventId,
    Guid UserId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
