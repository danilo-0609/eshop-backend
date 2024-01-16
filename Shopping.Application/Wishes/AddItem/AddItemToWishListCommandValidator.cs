using FluentValidation;

namespace Shopping.Application.Wishes.AddItem;

internal sealed class AddItemToWishListCommandValidator : AbstractValidator<AddItemToWishListCommand>
{
    public AddItemToWishListCommandValidator()
    {
        RuleFor(r => r.WishId)
            .NotNull();

        RuleFor(r => r.ItemId)
            .NotNull();
    }
}
