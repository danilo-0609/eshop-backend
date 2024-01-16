using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.Delete;

internal sealed class DeleteWishListCommandHandler : ICommandRequestHandler<DeleteWishListCommand, ErrorOr<Unit>>
{
    private readonly IWishRepository _wishRepository;

    public DeleteWishListCommandHandler(IWishRepository wishRepository)
    {
        _wishRepository = wishRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteWishListCommand request, CancellationToken cancellationToken)
    {
        Wish? wish = await _wishRepository.GetByIdAsync(WishId.Create(request.WishId));

        if (wish is null)
        {
            return Error.NotFound("Wish.NotFound", "Wish was not found");
        }

        await _wishRepository.DeleteAsync(wish);

        return Unit.Value;
    }
}
