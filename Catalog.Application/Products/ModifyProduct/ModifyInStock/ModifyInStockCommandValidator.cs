using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyInStock;
internal sealed class ModifyInStockCommandValidator : AbstractValidator<ModifyInStockCommand>
{
    public ModifyInStockCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.InStock)
            .NotNull().WithMessage("In stock cannot be null")
            .GreaterThanOrEqualTo(0).WithMessage("In stock cannot be less than 0");
    }
}