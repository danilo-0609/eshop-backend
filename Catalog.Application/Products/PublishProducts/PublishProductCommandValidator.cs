using FluentValidation;

namespace Catalog.Application.Products.PublishProducts;
internal sealed class PublishProductCommandValidator : AbstractValidator<PublishProductCommand>
{
    public PublishProductCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotNull().WithMessage("Name cannot be null")
            .NotEmpty().WithMessage("Name cannot be empty")
            .MaximumLength(300).WithMessage("Name length too long (must be less than 300 letters)");

        RuleFor(r => r.Price)
            .NotEmpty().WithMessage("Price cannot be empty")
            .NotNull().WithMessage("Price cannot be null")
            .LessThan(10000000m).WithMessage("Price too high");

        RuleFor(r => r.Description)
            .NotNull().WithMessage("Description cannot be null")
            .NotEmpty().WithMessage("Description cannot be empty")
            .MaximumLength(9000).WithMessage("Description too long");

        RuleFor(r => r.Size)
            .NotNull().WithMessage("Size cannot be null")
            .NotEmpty().WithMessage("Size cannot be empty")
            .MaximumLength(100);

        RuleFor(r => r.ProductType)
            .NotNull().WithMessage("Product type cannot be null")
            .NotEmpty().WithMessage("Product type cannot be empty")
            .MaximumLength(100);

        RuleFor(r => r.Tags)
            .NotNull().WithMessage("Tags cannot be null");

        RuleFor(r => r.InStock)
            .NotNull().WithMessage("In stock cannot be null")
            .NotEmpty().WithMessage("In stock cannot be empty");
    }
}

