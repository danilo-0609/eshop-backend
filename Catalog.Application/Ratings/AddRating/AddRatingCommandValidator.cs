using System.Data;
using FluentValidation;

namespace Catalog.Application.Ratings.AddRating;
internal sealed class AddRatingCommandValidator : AbstractValidator<AddRatingCommand>
{
    public AddRatingCommandValidator()
    {
        RuleFor(r => r.ProductId)
            .NotNull().WithMessage("Product id cannot be null")
            .NotEmpty().WithMessage("Product id cannot be empty");
    
        RuleFor(r => r.Rate)
            .NotNull().WithMessage("Rate cannot be null")
            .NotEmpty().WithMessage("Rate cannot be empty")
            .LessThan(5.1d).WithMessage("Rate must be a number between 1.0 and 5.0");
    }
}