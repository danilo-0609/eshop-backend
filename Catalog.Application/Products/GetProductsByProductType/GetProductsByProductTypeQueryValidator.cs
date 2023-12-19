using FluentValidation;

namespace Catalog.Application.Products.GetProductsByProductType;
internal sealed class GetProductsByProductTypeQueryValidator : AbstractValidator<GetProductsByProductTypeQuery>
{
    public GetProductsByProductTypeQueryValidator()
    {
        RuleFor(r => r.ProductType)
            .NotNull().WithMessage("Product type cannot be null")
            .NotEmpty().WithMessage("Product type cannot be empty");
    }
}
