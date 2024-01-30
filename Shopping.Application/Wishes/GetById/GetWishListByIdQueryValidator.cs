using FluentValidation;

namespace Shopping.Application.Wishes.GetById;

internal sealed class GetWishListByIdQueryValidator : AbstractValidator<GetWishListByIdQuery>
{
    public GetWishListByIdQueryValidator()
    {
        RuleFor(r => r.WishId)
            .NotNull();
    }
}
