using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record OrderConfirmedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    int AmountOfProducts,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
