using FluentValidation;

namespace Shopping.Application.Orders.Expire;

internal sealed class ExpireOrderCommandValidator : AbstractValidator<ExpireOrderCommand>
{
    public ExpireOrderCommandValidator()
    {
        RuleFor(r => r.OrderId)
            .NotNull();
    }
}
