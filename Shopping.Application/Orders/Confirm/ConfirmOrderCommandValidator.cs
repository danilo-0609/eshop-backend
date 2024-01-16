using FluentValidation;

namespace Shopping.Application.Orders.Confirm;

internal sealed class ConfirmOrderCommandValidator : AbstractValidator<ConfirmOrderCommand>
{
    public ConfirmOrderCommandValidator()
    {
        RuleFor(r => r.OrderId)
            .NotNull();
    }
}
