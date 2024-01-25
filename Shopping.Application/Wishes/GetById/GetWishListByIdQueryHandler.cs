using Shopping.Application.Common;
using ErrorOr;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.GetById;

internal sealed class GetWishListByIdQueryHandler : IQueryRequestHandler<GetWishListByIdQuery, ErrorOr<WishResponse>>
{
    private readonly IWishRepository _wishRepository;

    public GetWishListByIdQueryHandler(IWishRepository wishRepository)
    {
        _wishRepository = wishRepository;
    }

    public async Task<ErrorOr<WishResponse>> Handle(GetWishListByIdQuery request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return Error.NotFound("Wish.NotFound", "Wish was not found");
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
