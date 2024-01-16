using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public sealed record ProductSellFailedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    Guid OrderId,
    string FailureCause,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);