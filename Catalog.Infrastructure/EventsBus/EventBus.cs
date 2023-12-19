using BuildingBlocks.Application.EventBus;
using BuildingBlocks.Application.IntegrationEvents;
using MassTransit;

namespace Catalog.Infrastructure.EventsBus;
internal sealed class EventBus : IEventBus
{
    private readonly IPublishEndpoint _publishEndpoint;

    public EventBus(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task PublishAsync<T>(T @event) 
        where T : IntegrationEvent
    {
        await _publishEndpoint.Publish(@event);
    }
}