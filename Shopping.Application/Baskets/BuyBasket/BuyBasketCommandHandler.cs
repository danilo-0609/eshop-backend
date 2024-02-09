using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BuyBasketCommandHandler : ICommandRequestHandler<BuyBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IAuthorizationService _authorizationService;

    public BuyBasketCommandHandler(IBasketRepository basketRepository, IAuthorizationService authorizationService)
    {
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Guid>> Handle(BuyBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return BasketErrorCodes.NotFound;
        }

        var authorizeValidator = _authorizationService.IsUserAuthorized(basket.CustomerId);

        if (authorizeValidator.IsError)
        {
            return BasketErrorCodes.UserNotAuthorizedToAccess;
        }

        Dictionary<Guid, int> amountPerItems = await _basketRepository.GetBasketItemIdsAsync(basket.Id.Value);

        basket.Buy(amountPerItems);

        return basket.Id.Value;
    }
}

