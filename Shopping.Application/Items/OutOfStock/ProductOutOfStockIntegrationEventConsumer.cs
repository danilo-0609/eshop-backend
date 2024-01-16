using Catalog.IntegrationEvents;
using MassTransit;
using Shopping.Domain.Items;

namespace Shopping.Application.Items.OutOfStock;

public sealed class ProductOutOfStockIntegrationEventConsumer : IConsumer<ProductOutOfStockIntegrationEvent>
{
    private readonly IItemRepository _itemRepository;

    public ProductOutOfStockIntegrationEventConsumer(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Consume(ConsumeContext<ProductOutOfStockIntegrationEvent> context)
    {
        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(context.Message.ProductId));

        item!.OutOfStock();

        await _itemRepository.UpdateAsync(item);
    }
}
