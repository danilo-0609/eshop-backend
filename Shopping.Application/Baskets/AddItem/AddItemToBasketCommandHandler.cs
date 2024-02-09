using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

namespace Shopping.Application.Baskets.AddItem;

internal sealed class AddItemToBasketCommandHandler : ICommandRequestHandler<AddItemToBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IAuthorizationService _authorizationService;

    public AddItemToBasketCommandHandler(IItemRepository itemRepository, IBasketRepository basketRepository, IAuthorizationService authorizationService)
    {
        _itemRepository = itemRepository;
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Guid>> Handle(AddItemToBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return BasketErrorCodes.NotFound;
        }

        var authorizeService = _authorizationService.IsUserAuthorized(basket.CustomerId);

        if (authorizeService.IsError)
        {
            return BasketErrorCodes.UserNotAuthorizedToAccess;
        }

        Item? item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return ItemErrorCodes.NotFound;
        }

        var update = basket.Update(DateTime.UtcNow, item.Id, basket.AmountOfProducts, request.Amount);

        await _basketRepository.UpdateAsync(update);

        return basket.Id.Value;
    }
}