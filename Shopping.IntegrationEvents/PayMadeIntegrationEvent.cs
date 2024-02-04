using BuildingBlocks.Application.IntegrationEvents;

namespace Shopping.IntegrationEvents;

public sealed record PayMadeIntegrationEvent(
    Guid IntegrationEventId,
    Guid PaymentId,
    Guid OrderId,
    Guid PayerId,
    decimal MoneyAmount,
    Guid ProductId,
    DateTime OcurredOn) : IntegrationEvent(IntegrationEventId, OcurredOn);
