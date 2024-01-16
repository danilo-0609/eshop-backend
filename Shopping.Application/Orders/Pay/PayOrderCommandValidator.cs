using FluentValidation;

namespace Shopping.Application.Orders.Pay;

internal sealed class PayOrderCommandValidator : AbstractValidator<PayOrderCommand>
{
    public PayOrderCommandValidator()
    {
        RuleFor(r => r.OrderId)
            .NotNull();
    }
}
