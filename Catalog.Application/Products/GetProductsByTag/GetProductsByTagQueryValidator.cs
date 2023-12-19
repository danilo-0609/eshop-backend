using FluentValidation;

namespace Catalog.Application.Products.GetProductsByTag;
internal sealed class GetProductsByTagQueryValidator : AbstractValidator<GetProductsByTagQuery>
{
    public GetProductsByTagQueryValidator()
    {
        RuleFor(r => r.Tag)
            .NotNull().WithMessage("Tag cannot be null")
            .NotEmpty().WithMessage("Tag cannot be empty");
    }
}
