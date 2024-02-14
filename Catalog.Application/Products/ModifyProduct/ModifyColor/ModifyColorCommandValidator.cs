using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyColor;
internal sealed class ModifyColorCommandValidator : AbstractValidator<ModifyColorCommand>
{
    public ModifyColorCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Colors)
            .NotNull().WithMessage("Color cannot be null")
            .NotEmpty().WithMessage("Color cannot be empty");
    }
}