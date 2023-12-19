using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;

public sealed record ProductPublishedIntegrationEvent(Guid IntegrationEventId,
    Guid ProductId,
    Guid SellerId,
    string Name,
    string Description,
    decimal Price,
    int InStock,
    string Size,
    string ProductType,
    List<string> Tags,
    DateTime OcurredOn,
    string Color = "") : IntegrationEvent(IntegrationEventId, OcurredOn);
