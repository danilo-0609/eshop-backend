using BuildingBlocks.Application.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shopping.Application.Common;

namespace Shopping.Infrastructure.EventsBus;

internal sealed class EventBus : IShoppingEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<EventBus> _logger;

    public EventBus(IPublishEndpoint publishEndpoint, ILogger<EventBus> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        _logger.LogInformation("Event bus starting publishing integration event, {event}, {OcurredOn}",
            @event.GetType().Name,
            DateTime.UtcNow);

        await _publishEndpoint.Publish(@event);

        _logger.LogInformation("Integration event has published in event bus already, {event}, {OcurredOn}",
            @event.GetType().Name,
            DateTime.UtcNow);
    }
}