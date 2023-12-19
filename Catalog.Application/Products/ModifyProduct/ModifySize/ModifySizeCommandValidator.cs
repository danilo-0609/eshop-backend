using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifySize;
internal sealed class ModifySizeCommandValidator : AbstractValidator<ModifySizeCommand>
{
    public ModifySizeCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Size)
            .NotNull().WithMessage("Size cannot be null")
            .NotEmpty().WithMessage("Size cannot be empty")
            .MaximumLength(100);
    }
}