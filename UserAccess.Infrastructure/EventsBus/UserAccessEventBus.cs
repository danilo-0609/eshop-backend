using BuildingBlocks.Application.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using UserAccess.Application.Abstractions;

namespace UserAccess.Infrastructure.EventsBus;

internal sealed class UserAccessEventBus : IUserAccessEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<UserAccessEventBus> _logger;

    public UserAccessEventBus(IPublishEndpoint publishEndpoint, ILogger<UserAccessEventBus> logger)
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
