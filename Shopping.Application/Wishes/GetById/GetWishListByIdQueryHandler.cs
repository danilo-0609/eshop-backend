using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.GetById;

internal sealed class GetWishListByIdQueryHandler : IQueryRequestHandler<GetWishListByIdQuery, ErrorOr<WishResponse>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetWishListByIdQueryHandler(IWishRepository wishRepository, IAuthorizationService authorizationService)
    {
        _wishRepository = wishRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<WishResponse>> Handle(GetWishListByIdQuery request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return WishErrorCodes.NotFound;
        }

        if (wish.IsPrivate is false)
        {
            var authorizationService = _authorizationService.IsUserAuthorized(wish.CustomerId);

            if (authorizationService.IsError)
            {
                return WishErrorCodes.UserNotAuthorizedToAccess;
            }
        }

        WishResponse wishResponse = new(
            wish.Id.Value,
            wish.Items.ConvertAll(id => id.Value),
            wish.Name,
            wish.IsPrivate,
            wish.CreatedOn);

        return wishResponse;
    }
}
