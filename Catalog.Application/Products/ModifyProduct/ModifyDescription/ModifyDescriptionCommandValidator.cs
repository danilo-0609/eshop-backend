using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyDescription;

internal sealed class ModifyDescriptionCommandValidator : AbstractValidator<ModifyDescriptionCommand>
{
    public ModifyDescriptionCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Description)
            .NotNull().WithMessage("Description cannot be null")
            .NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(9000).WithMessage("Description too long");
    }
}