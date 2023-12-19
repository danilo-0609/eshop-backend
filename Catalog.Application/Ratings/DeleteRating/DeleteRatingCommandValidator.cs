using FluentValidation;

namespace Catalog.Application.Ratings.DeleteRating;

internal sealed class DeleteRatingCommandValidator : AbstractValidator<DeleteRatingCommand>
{
    public DeleteRatingCommandValidator()
    {
        RuleFor(r => r.Id)
            .NotNull().WithMessage("Id cannot be null")
            .NotEmpty().WithMessage("Id cannot be empty");
    }
}