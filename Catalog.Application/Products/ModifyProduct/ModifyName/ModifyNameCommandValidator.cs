using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyName;
internal sealed class ModifyNameCommandValidator : AbstractValidator<ModifyNameCommand>
{
    public ModifyNameCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(300).WithMessage("Name length too long (must be less than 300 letters)");
    }
}