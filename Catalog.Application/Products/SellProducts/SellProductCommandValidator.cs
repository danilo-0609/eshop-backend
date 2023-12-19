using FluentValidation;

namespace Catalog.Application.Products.SellProducts;
internal sealed class SellProductCommandValidator : AbstractValidator<SellProductCommand>
{
    public SellProductCommandValidator()
    {
        RuleFor(r => r.AmountOfProducts)
            .NotNull().WithMessage("Selling amount of products cannot be null")
            .NotEmpty().WithMessage("Selling amount of products cannot be empty");

        RuleFor(r => r.ProductId)
            .NotEmpty().WithMessage("Product id cannot be empty")
            .NotNull().WithMessage("Product id cannot be null");
    }
}
