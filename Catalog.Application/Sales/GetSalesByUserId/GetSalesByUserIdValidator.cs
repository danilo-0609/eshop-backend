using Catalog.Application.Sales.GetAllSalesByUserId;
using FluentValidation;

namespace Catalog.Application.Sales.GetSalesByUserId;
internal sealed class GetSalesByUserIdValidator : AbstractValidator<GetSalesByUserIdQuery>
{
    public GetSalesByUserIdValidator()
    {
        RuleFor(r => r.UserId)
            .NotNull().WithMessage("User id cannot be null")
            .NotEmpty().WithMessage("User id cannot be empty");
    }
}