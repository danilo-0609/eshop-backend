using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record BuyGeneratedIntegrationEvent(
    Guid IntegrationEventId,
    Guid BuyId,
    Guid BuyerId,
    Guid ProductId,
    decimal UnitPrice,
    decimal TotalPrice,
    int AmountOfProducts,  
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
