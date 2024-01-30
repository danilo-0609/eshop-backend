using BuildingBlocks.Application;
using FluentValidation;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.DeleteBasket;

internal sealed class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator(IBasketRepository basketRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.BasketId)
            .NotNull();
    }
}
