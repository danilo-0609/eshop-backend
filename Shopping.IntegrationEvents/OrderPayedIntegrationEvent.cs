using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record OrderPayedIntegrationEvent(
    Guid IntegrationEventId,
    Guid ProductId,
    Guid OrderId,
    int AmountOfProducts,
    DateTime CreatedOn) : IntegrationEvent(IntegrationEventId, CreatedOn);
