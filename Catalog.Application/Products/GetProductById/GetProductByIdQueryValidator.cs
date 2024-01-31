using FluentValidation;

namespace Catalog.Application.Products.GetProductById;

internal sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull();
    }
}
