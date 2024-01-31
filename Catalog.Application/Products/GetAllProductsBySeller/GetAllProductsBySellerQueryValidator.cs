using FluentValidation;

namespace Catalog.Application.Products.GetAllProductsBySeller;

internal sealed class GetAllProductsBySellerQueryValidator : AbstractValidator<GetAllProductsBySellerQuery>
{
    public GetAllProductsBySellerQueryValidator()
    {
        RuleFor(d => d.SellerId)
            .NotNull().WithMessage("Seller id cannot be null")
            .NotEmpty().WithMessage("Seller id cannot be empty");
    }
}
