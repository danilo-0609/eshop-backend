using BuildingBlocks.Application.IntegrationEvents;

namespace Catalog.IntegrationEvents;
public record ProductOutOfStockIntegrationEvent(Guid IntegrationEventId,
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);

   

