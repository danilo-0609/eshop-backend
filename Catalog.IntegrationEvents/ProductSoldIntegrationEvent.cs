using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;

public sealed record ProductSoldIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    int Amount,
    decimal UnitPrice,
    Guid OrderId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);