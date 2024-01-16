using Catalog.IntegrationEvents;
using MassTransit;
using Shopping.Domain.Items;
using System.Net.Mime;

namespace Shopping.Application.Items.Update;

public sealed class ProductUpdatedIntegrationConsumer : IConsumer<ProductUpdatedIntegrationEvent>
{
    private readonly IItemRepository _itemRepository;

    public ProductUpdatedIntegrationConsumer(IItemRepository itemRepository)
    {
        _itemRepository = itemRepository;
    }

    public async Task Consume(ConsumeContext<ProductUpdatedIntegrationEvent> context)
    {
        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(context.Message.ProductId));

        Item update = Item.Update(
            context.Message.ProductId,
            item!.Name,
            item.SellerId,
            item.Price,
            item.InStock,
            item.CreatedOn,
            DateTime.UtcNow);

        await _itemRepository.UpdateAsync(update);
    }
}
