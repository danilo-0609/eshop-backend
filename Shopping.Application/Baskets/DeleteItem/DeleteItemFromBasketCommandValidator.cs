using FluentValidation;

namespace Shopping.Application.Baskets.DeleteItem;

internal sealed class DeleteItemFromBasketCommandValidator : AbstractValidator<DeleteItemFromBasketCommand>
{
    public DeleteItemFromBasketCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .NotNull();

        RuleFor(r => r.BasketId)
            .NotNull();
    }
}

