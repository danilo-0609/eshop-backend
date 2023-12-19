using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyProductType;
internal sealed class ModifyProductTypeCommandValidator : AbstractValidator<ModifyProductTypeCommand>
{
    public ModifyProductTypeCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.ProductType)
            .NotNull().WithMessage("Product type cannot be null")
            .NotEmpty().WithMessage("Product type cannot be empty")
            .MaximumLength(100);
    }
}
