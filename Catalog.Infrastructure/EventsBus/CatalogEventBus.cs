using BuildingBlocks.Application.IntegrationEvents;
using Catalog.Application.Common;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Catalog.Infrastructure.EventsBus;

internal sealed class CatalogEventBus : ICatalogEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<CatalogEventBus> _logger;

    public CatalogEventBus(IPublishEndpoint publishEndpoint, ILogger<CatalogEventBus> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task PublishAsync<T>(T @event) 
        where T : IntegrationEvent
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