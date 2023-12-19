using FluentValidation;

namespace Catalog.Application.Products.RemoveProduct;
internal sealed class RemoveProductCommandValidator : AbstractValidator<RemoveProductCommand>
{
    public RemoveProductCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotEmpty().WithMessage("Id cannot be empty")
            .NotNull().WithMessage("Id cannot be null");
    }
}

