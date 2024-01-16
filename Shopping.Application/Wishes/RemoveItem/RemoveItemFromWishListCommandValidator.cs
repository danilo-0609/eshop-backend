using FluentValidation;

namespace Shopping.Application.Wishes.RemoveItem;

internal sealed class RemoveItemFromWishListCommandValidator : AbstractValidator<RemoveItemFromWishListCommand>
{
    public RemoveItemFromWishListCommandValidator()
    {
        RuleFor(r => r.WishId)
            .NotNull();

        RuleFor(r => r.ItemId)
            .NotNull();
    }
}
