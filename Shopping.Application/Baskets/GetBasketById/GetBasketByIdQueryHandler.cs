using BuildingBlocks.Application.Queries;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.GetBasketById;

internal sealed class GetBasketByIdQueryHandler : IQueryRequestHandler<GetBasketByIdQuery, ErrorOr<BasketResponse>>
{
    private readonly IBasketRepository _basketRepository;

    public GetBasketByIdQueryHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<BasketResponse>> Handle(GetBasketByIdQuery request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return Error.NotFound("Basket.NotFound", "Basket was not found");
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

