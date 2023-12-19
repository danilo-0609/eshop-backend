using FluentValidation;

namespace Catalog.Application.Products.ModifyProduct.ModifyTag;
internal sealed class ModifyTagCommandValidator : AbstractValidator<ModifyTagCommand>
{
    public ModifyTagCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");

        RuleFor(r => r.Tags)
            .NotNull().WithMessage("Tags cannot be null");
    }
}