using BuildingBlocks.Application;
using FluentValidation;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.DeleteItem;

internal sealed class DeleteItemFromBasketCommandValidator : AbstractValidator<DeleteItemFromBasketCommand>
{
    public DeleteItemFromBasketCommandValidator(IBasketRepository basketRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.ItemId)
            .NotNull();

        RuleFor(r => r.BasketId)
            .NotNull();
    }
}

