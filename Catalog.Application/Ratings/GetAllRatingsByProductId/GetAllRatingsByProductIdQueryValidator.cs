using Catalog.Application.Ratings.Catalog.Application.Ratings.GetAllRatingsByProductId;
using FluentValidation;

namespace Catalog.Application.Ratings.GetAllRatingsByProductId;
internal sealed class GetAllRatingsByProductIdQueryValidator : AbstractValidator<GetAllRatingsByProductIdQuery>
{
    public GetAllRatingsByProductIdQueryValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");
    }
}
