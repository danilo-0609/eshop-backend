using FluentValidation;

namespace Shopping.Application.Baskets.CreateBasket;

internal sealed class CreateBasketCommandValidator : AbstractValidator<CreateBasketCommand>
{
    public CreateBasketCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .NotNull();
    }
}

