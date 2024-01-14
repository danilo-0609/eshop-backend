using FluentValidation;

namespace Shopping.Application.Baskets.GetBasketById;

internal sealed class GetBasketByIdQueryValidator : AbstractValidator<GetBasketByIdQuery>
{
    public GetBasketByIdQueryValidator()
    {
        RuleFor(r => r.BasketId)
            .NotNull();
    }
}
