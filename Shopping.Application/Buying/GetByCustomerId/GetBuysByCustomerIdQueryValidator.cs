using FluentValidation;

namespace Shopping.Application.Buying.GetByCustomerId;

internal sealed class GetBuysByCustomerIdQueryValidator : AbstractValidator<GetBuysByCustomerIdQuery>
{
    public GetBuysByCustomerIdQueryValidator()
    {
        RuleFor(r => r.CustomerId)
            .NotNull();
    }
}
