using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;

public sealed record ProductPublishedIntegrationEvent(Guid IntegrationEventId,
    Guid ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
