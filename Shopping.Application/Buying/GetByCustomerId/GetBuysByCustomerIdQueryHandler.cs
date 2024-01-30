using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Buying;

namespace Shopping.Application.Buying.GetByCustomerId;

internal sealed class GetBuysByCustomerIdQueryHandler : IQueryRequestHandler<GetBuysByCustomerIdQuery, ErrorOr<IReadOnlyList<BuyResponse>>>
{
    private readonly IBuyRepository _buyRepository;
    private readonly BuyAuthorizationService _authorizationService;

    public GetBuysByCustomerIdQueryHandler(IBuyRepository buyRepository, BuyAuthorizationService authorizationService)
    {
        _buyRepository = buyRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<IReadOnlyList<BuyResponse>>> Handle(GetBuysByCustomerIdQuery request, CancellationToken cancellationToken)
    {
        List<Buy>? buys = await _buyRepository.GetBuysByCustomerId(request.CustomerId);

        if (buys is null)
        {
            return BuyErrorCodes.NotFound;
        }

        var authorizeService = _authorizationService.IsUserAuthorized(buys.First().BuyerId);

        if (authorizeService.IsError && _authorizationService.IsAdmin() is false)
        {
            return authorizeService.FirstError;
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
