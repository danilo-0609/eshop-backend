using FluentValidation;

namespace Catalog.Application.Products.GetProductsByName;
internal sealed class GetProductsByNameQueryValidator : AbstractValidator<GetProductsByNameQuery>
{
    public GetProductsByNameQueryValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .NotNull().WithMessage("Name cannot be null");
    }
}
