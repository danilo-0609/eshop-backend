using Catalog.IntegrationEvents;
using MassTransit;
using Shopping.Domain.Items;

namespace Shopping.Application.Items.Create;

public sealed class ProductPublishedIntegrationEventConsumer : IConsumer<ProductPublishedIntegrationEvent>
{
    private readonly IItemRepository _itemRepository;

    public ProductPublishedIntegrationEventConsumer(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Consume(ConsumeContext<ProductPublishedIntegrationEvent> context)
    {
        Item item = Item.Create(
            context.Message.ProductId,
            context.Message.Name,
            context.Message.SellerId,
            context.Message.Price,
            context.Message.InStock,
            DateTime.UtcNow);
    
        await _itemRepository.AddAsync(item);
    }
}
