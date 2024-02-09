using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

namespace Shopping.Application.Baskets.DeleteItem;

internal sealed class DeleteItemFromBasketCommandHandler : ICommandRequestHandler<DeleteItemFromBasketCommand, ErrorOr<Unit>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IAuthorizationService _authorizationService;
    private readonly IItemRepository _itemRepository;

    public DeleteItemFromBasketCommandHandler(IBasketRepository basketRepository, IAuthorizationService authorizationService, IItemRepository itemRepository)
    {
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteItemFromBasketCommand request, CancellationToken cancellationToken)
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

        Dictionary<Guid, int>? items = await _basketRepository.GetBasketItemIdsAsync(request.BasketId);

        if (!items.Any(r => r.Key == request.ItemId))
        {
            return ItemErrorCodes.NotFound;
        }

        var item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        await _basketRepository.DeleteItemInBasketIdAsync(
            request.ItemId, 
            request.BasketId, 
            basket.AmountOfProducts - items[request.ItemId],
            basket.TotalAmount - item!.Price);

        return Unit.Value;
    }
}

