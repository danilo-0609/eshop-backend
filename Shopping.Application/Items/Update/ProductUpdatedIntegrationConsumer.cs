using Catalog.IntegrationEvents;
using MassTransit;
using Microsoft.Extensions.Logging;
using Shopping.Domain.Items;

namespace Shopping.Application.Items.Update;

public sealed class ProductUpdatedIntegrationConsumer : IConsumer<ProductUpdatedIntegrationEvent>
{
    private readonly IItemRepository _itemRepository;
    private readonly ILogger<ProductUpdatedIntegrationConsumer> _logger;

    public ProductUpdatedIntegrationConsumer(IItemRepository itemRepository, ILogger<ProductUpdatedIntegrationConsumer> logger)
    {
        _itemRepository = itemRepository;
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<ProductUpdatedIntegrationEvent> context)
    {
        _logger.LogInformation("Starting consuming {Event}, {DateTime}",
            typeof(ProductUpdatedIntegrationEvent).FullName,
            DateTime.UtcNow);

        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(context.Message.ProductId));

        if (item is null)
        {
            return;
        }

        Item update = Item.Update(
            context.Message.ProductId,
            context.Message.Name,
            context.Message.SellerId,
            context.Message.Price,
            context.Message.InStock,
            item!.CreatedOn,
            DateTime.UtcNow);

        await _itemRepository.UpdateAsync(update);

        _logger.LogInformation("Consuming finished {Event}, {DateTime}",
            typeof(ProductUpdatedIntegrationEvent).Name,
            DateTime.UtcNow);
    }
}
