using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public record ProductUpdatedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    string Name,
    Guid SellerId,
    decimal Price,
    int InStock,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);