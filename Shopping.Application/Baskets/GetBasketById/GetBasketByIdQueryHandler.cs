using ErrorOr;
using MassTransit.Initializers;
using Shopping.Application.Common;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.GetBasketById;

internal sealed class GetBasketByIdQueryHandler : IQueryRequestHandler<GetBasketByIdQuery, ErrorOr<BasketResponse>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetBasketByIdQueryHandler(IBasketRepository basketRepository, IAuthorizationService authorizationService)
    {
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<BasketResponse>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return BasketErrorCodes.NotFound;
        }

        var authorizeService = _authorizationService.IsUserAuthorized(basket.CustomerId);

        if (authorizeService.IsError && _authorizationService.IsAdmin() is false)
        {
            return BasketErrorCodes.UserNotAuthorizedToAccess;
        }

        Dictionary<Guid, int> itemIds = await _basketRepository.GetBasketItemIdsAsync(request.BasketId);

        BasketResponse basketResponse = new(
            basket.Id.Value,
            basket.CustomerId,
            itemIds.AsReadOnly(),
            basket.AmountOfProducts,
            basket.TotalAmount,
            basket.CreatedOn);

        return basketResponse;
    }
}
