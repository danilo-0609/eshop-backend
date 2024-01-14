using FluentValidation;

namespace Shopping.Application.Baskets.AddItem;

internal sealed class AddItemToBasketCommandValidator : AbstractValidator<AddItemToBasketCommand>
{
    public AddItemToBasketCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .NotNull();

        RuleFor(r => r.BasketId)
            .NotNull();
    }
}
