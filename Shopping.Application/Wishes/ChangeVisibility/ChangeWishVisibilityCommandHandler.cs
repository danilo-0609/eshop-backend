using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.ChangeVisibility;

internal sealed class ChangeWishVisibilityCommandHandler : ICommandRequestHandler<ChangeWishVisibilityCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;

    public ChangeWishVisibilityCommandHandler(IWishRepository wishRepository)
    {
        _wishRepository = wishRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeWishVisibilityCommand request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return Error.NotFound("Wish.NotFound", "Wish was not found");
        }

        wish.ChangeVisibility(request.Visibility);

        await _wishRepository.UpdateAsync(wish);

        return Unit.Value;
    }
}
