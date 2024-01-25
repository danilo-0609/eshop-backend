using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Buying;

namespace Shopping.Application.Buying.GetByCustomerId;

internal sealed class GetBuysByCustomerIdQueryHandler : IQueryRequestHandler<GetBuysByCustomerIdQuery, ErrorOr<IReadOnlyList<BuyResponse>>>
{
    private readonly IBuyRepository _buyRepository;

    public GetBuysByCustomerIdQueryHandler(IBuyRepository buyRepository)
    {
        _buyRepository = buyRepository;
    }

    public async Task<ErrorOr<IReadOnlyList<BuyResponse>>> Handle(GetBuysByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<Buy>? buys = await _buyRepository.GetBuysByCustomerId(request.CustomerId);

        if (buys is null)
        {
            return Error.NotFound("Buys.NotFound", "Buys were not found");
        }

        var buyResponses = buys
            .ConvertAll(
                buyResponse =>
                    new BuyResponse(buyResponse!.Id.Value,
                        buyResponse.ItemId.Value,
                        buyResponse.AmountOfProducts,
                        buyResponse.UnitPrice,
                        buyResponse.TotalAmount,
                        buyResponse.OcurredOn))
            .ToList()
            .AsReadOnly();

        return buyResponses;
    }
}
