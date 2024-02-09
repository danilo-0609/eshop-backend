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

    public DeleteItemFromBasketCommandHandler(IBasketRepository basketRepository, IAuthorizationService authorizationService)
    {
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
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

        if (!basket.ItemIds.Any(r => r.Value == request.ItemId))
        {
            return ItemErrorCodes.NotFound;
        }

        basket.RemoveItem(ItemId.Create(request.ItemId));

        return Unit.Value;
    }
}

