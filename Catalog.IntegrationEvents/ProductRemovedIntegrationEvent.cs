using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public sealed record ProductRemovedIntegrationEvent(Guid IntegrationEventId, 
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);