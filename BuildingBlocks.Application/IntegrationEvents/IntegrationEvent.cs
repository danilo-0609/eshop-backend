namespace BuildingBlocks.Application.IntegrationEvents;

public abstract record IntegrationEvent(Guid IntegrationEventId, DateTime OcurredOn);
