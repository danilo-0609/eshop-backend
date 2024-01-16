using Catalog.IntegrationEvents;
using MassTransit;
using Shopping.Domain.Items;

namespace Shopping.Application.Items.Remove;

public sealed class ProductRemovedIntegrationEventConsumer : IConsumer<ProductRemovedIntegrationEvent>
{
    private readonly IItemRepository _itemRepository;

    public ProductRemovedIntegrationEventConsumer(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Consume(ConsumeContext<ProductRemovedIntegrationEvent> context)
    {
        await _itemRepository.DeleteAsync(ItemId.Create(context.Message.ProductId));
    }
}
