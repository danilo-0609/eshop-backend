using FluentValidation;

namespace Shopping.Application.Orders.Place;

internal sealed class PlaceOrderCommandValidator : AbstractValidator<PlaceOrderCommand>
{
    public PlaceOrderCommandValidator()
    {
        RuleFor(r => r.ItemId)
            .NotNull();

        RuleFor(r => r.AmountRequested)
            .NotNull()
            .NotEmpty();
    }
}
