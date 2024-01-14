using FluentValidation;

namespace Shopping.Application.Baskets.DeleteBasket;

internal sealed class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
{
    public DeleteBasketCommandValidator()
    {
        RuleFor(r => r.BasketId)
            .NotNull();
    }
}
