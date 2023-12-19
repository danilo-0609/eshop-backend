using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public sealed record ProductSellFailedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    string FailureCause,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);