using BuildingBlocks.Application.Queries;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.GetBasketById;

internal sealed class GetBasketByIdQueryHandler : IQueryRequestHandler<GetBasketByIdQuery, ErrorOr<BasketResponse>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly AuthorizationService _authorizationService;

    public GetBasketByIdQueryHandler(IBasketRepository basketRepository, AuthorizationService authorizationService)
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

        BasketResponse basketResponse = new(
            basket.Id.Value,
            basket.CustomerId,
            basket.ItemIds
                .Select(r => r.Value)
                .ToList()
                .AsReadOnly(),
            basket.AmountOfProducts,
            basket.TotalAmount,
            basket.CreatedOn);

        return basketResponse;
    }
}
