using FluentValidation;

namespace Shopping.Application.Wishes.ChangeVisibility;

internal sealed class ChangeWishVisibilityCommandValidator : AbstractValidator<ChangeWishVisibilityCommand>
{
    public ChangeWishVisibilityCommandValidator()
    {
        RuleFor(r => r.WishId)
            .NotNull();

        RuleFor(r => r.Visibility)
            .NotNull();
    }
}
