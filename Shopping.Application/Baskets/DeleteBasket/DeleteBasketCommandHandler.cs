using BuildingBlocks.Application.Commands;
using ErrorOr;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.DeleteBasket;

internal sealed class DeleteBasketCommandHandler : ICommandRequestHandler<DeleteBasketCommand, ErrorOr<Guid>>
{
    private readonly IBasketRepository _basketRepository;

    public DeleteBasketCommandHandler(IBasketRepository basketRepository)
    {
        _basketRepository = basketRepository;
    }

    public async Task<ErrorOr<Guid>> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
    {
        Basket? basket = await _basketRepository.GetByIdAsync(BasketId.Create(request.BasketId));

        if (basket is null)
        {
            return Error.NotFound("Basket.NotFound", "Basket was not found");
        }

        await _basketRepository.DeleteAsync(basket);

        return basket.Id.Value;
    }
}
