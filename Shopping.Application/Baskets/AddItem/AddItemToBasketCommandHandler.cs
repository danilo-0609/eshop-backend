using BuildingBlocks.Application.Commands;
using ErrorOr;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

namespace Shopping.Application.Baskets.AddItem;

internal sealed class AddItemToBasketCommandHandler : ICommandRequestHandler<AddItemToBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IItemRepository _itemRepository;

    public AddItemToBasketCommandHandler(IItemRepository itemRepository, IBasketRepository basketRepository)
    {
        _itemRepository = itemRepository;
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return Error.NotFound("Basket.NotFound", "Basket was not found");
        }

        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", "Item was not found. Cannot be added to the basket");
        }

        basket.AddItem(item.Id);

        await _basketRepository.UpdateAsync(basket);

        return basket.Id.Value;
    }
}