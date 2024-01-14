using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using Shopping.Domain.Basket;
using Shopping.Domain.Items;

namespace Shopping.Application.Baskets.DeleteItem;

internal sealed class DeleteItemFromBasketCommandHandler : ICommandRequestHandler<DeleteItemFromBasketCommand, ErrorOr<Unit>>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteItemFromBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(DeleteItemFromBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return Error.NotFound("Basket.NotFound", "Basket was not found");
        }

        if (!basket.ItemIds.Any(r => r.Value == request.ItemId))
        {
            return Error.NotFound("Item.NotFoundInBasket", "Item was not found in the basket");
        }

        basket.RemoveItem(ItemId.Create(request.ItemId));

        return Unit.Value;
    }
}

