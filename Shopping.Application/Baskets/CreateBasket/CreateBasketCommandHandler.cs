using BuildingBlocks.Application.Commands;
using BuildingBlocks.Application;
using ErrorOr;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

namespace Shopping.Application.Baskets.CreateBasket;

internal sealed class CreateBasketCommandHandler : ICommandRequestHandler<CreateBasketCommand, ErrorOr<Guid>>
{
    private readonly IItemRepository _itemRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IBasketRepository _basketRepository;

    public CreateBasketCommandHandler(IItemRepository itemRepository, IExecutionContextAccessor executionContextAccessor = null, IBasketRepository basketRepository = null)
    {
        _itemRepository = itemRepository;
        _executionContextAccessor = executionContextAccessor;
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateBasketCommand request, CancellationToken cancellationToken)
    {
        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return Error.NotFound("Item.NotFound", "Item was not found and cannot be added to the basket");
        }

        Basket basket = Basket.Create(_executionContextAccessor.UserId,
            item.Id,
            item.Price,
            DateTime.UtcNow);

        await _basketRepository.AddAsync(basket);

        return basket.Id.Value;
    }
}
