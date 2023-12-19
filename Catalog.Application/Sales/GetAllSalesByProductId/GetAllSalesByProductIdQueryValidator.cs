using FluentValidation;

namespace Catalog.Application.Sales.GetAllSalesByProductId;
internal sealed class GetAllSalesByProductIdQueryValidator : AbstractValidator<GetAllSalesByProductIdQuery>
{
    public GetAllSalesByProductIdQueryValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");
    }
}
