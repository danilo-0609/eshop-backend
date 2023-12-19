using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public record ProductUpdatedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);