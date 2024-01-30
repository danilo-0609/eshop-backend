using BuildingBlocks.Application;
using FluentValidation;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BuyBasketCommandValidator : AbstractValidator<BuyBasketCommand>
{
    public BuyBasketCommandValidator(IBasketRepository basketRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.BasketId)
            .NotNull();
}

