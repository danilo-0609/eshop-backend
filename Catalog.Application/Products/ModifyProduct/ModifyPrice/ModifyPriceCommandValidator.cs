using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyPrice;
internal sealed class ModifyPriceCommandValidator : AbstractValidator<ModifyPriceCommand>
{
    public ModifyPriceCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Price)
            .NotEmpty().WithMessage("Price cannot be empty")
            .NotNull().WithMessage("Price cannot be null")
            .LessThan(10000000m).WithMessage("Price too high");
    }
}