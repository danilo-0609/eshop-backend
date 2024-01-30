using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.DeleteBasket;

internal sealed class DeleteBasketCommandHandler : ICommandRequestHandler<DeleteBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;
    private readonly IAuthorizationService _authorizationService;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository, IAuthorizationService authorizationService)
    {
        _basketRepository = basketRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Guid>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
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

        await _basketRepository.DeleteAsync(basket);

        return basket.Id.Value;
    }
}
