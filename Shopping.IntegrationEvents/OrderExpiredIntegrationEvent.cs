using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record OrderExpiredIntegrationEvent(
    Guid IntegrationEventId,
    Guid OrderId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
