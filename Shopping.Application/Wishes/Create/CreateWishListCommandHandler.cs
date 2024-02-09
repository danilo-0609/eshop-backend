using BuildingBlocks.Application;
using Shopping.Application.Common;
using ErrorOr;
using MediatR;
using Shopping.Domain.Items;
using Shopping.Domain.Wishes;

namespace Shopping.Application.Wishes.Create;

internal sealed class CreateWishListCommandHandler : ICommandRequestHandler<CreateWishListCommand, ErrorOr<Guid>>
{
    private readonly IWishRepository _wishRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;
    private readonly IItemRepository _itemRepository;

    public CreateWishListCommandHandler(IWishRepository wishRepository, IExecutionContextAccessor executionContextAccessor, IItemRepository itemRepository)
    {
        _wishRepository = wishRepository;
        _executionContextAccessor = executionContextAccessor;
        _itemRepository = itemRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(CreateWishListCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(ItemId.Create(request.ItemId));

        if (item is null)
        {
            return ItemErrorCodes.NotFound;
        }

        Wish wish = Wish.Create(
            _executionContextAccessor.UserId,
            item.Id,
            request.Name,
            request.IsPrivate,
            DateTime.UtcNow);

        await _wishRepository.AddAsync(wish);

        return wish.Id.Value;
    }
}
