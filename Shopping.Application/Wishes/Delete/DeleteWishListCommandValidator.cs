using FluentValidation;

namespace Shopping.Application.Wishes.Delete;

internal sealed class DeleteWishListCommandValidator : AbstractValidator<DeleteWishListCommand>
{
    public DeleteWishListCommandValidator()
    {
        RuleFor(r => r.WishId)
            .NotNull();
    }
}
