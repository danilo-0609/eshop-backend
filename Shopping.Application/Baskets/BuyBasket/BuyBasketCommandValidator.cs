﻿using FluentValidation;

namespace Shopping.Application.Baskets.BuyBasket;

internal sealed class BuyBasketCommandValidator : AbstractValidator<BuyBasketCommand>
{
    public BuyBasketCommandValidator()
    {
        RuleFor(r => r.BasketId)
            .NotNull();
    }
}

