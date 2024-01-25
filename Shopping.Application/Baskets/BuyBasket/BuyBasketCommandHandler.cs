using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BuyBasketCommandHandler : ICommandRequestHandler<BuyBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;

    public BuyBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(BuyBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return Error.NotFound("Basket.NotFound", "Basket was not found");
        }

        basket.Buy();

        return basket.Id.Value;
    }
}

