using FluentValidation;

namespace Shopping.Application.Orders.GetById;

internal sealed class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(r => r.OrderId)
            .NotNull();
    }
}
