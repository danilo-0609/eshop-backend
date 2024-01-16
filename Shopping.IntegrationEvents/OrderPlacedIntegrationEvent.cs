using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record OrderPlacedIntegrationEvent(
    Guid IntegrationEventId,
    Guid OrderId,
    Guid CustomerId,
    Guid ItemId,
    string OrderStatus,
    int AmountRequested,
    decimal TotalMoneyAmount,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn); 
