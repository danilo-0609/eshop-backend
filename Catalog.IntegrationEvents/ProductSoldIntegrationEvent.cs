using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public sealed record ProductSoldIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    int Amount,
    decimal UnitPrice,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);