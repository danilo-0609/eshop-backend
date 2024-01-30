using BuildingBlocks.Application;
using FluentValidation;
using Shopping.Domain.Basket;

namespace Shopping.Application.Baskets.GetBasketById;

internal sealed class GetBasketByIdQueryValidator : AbstractValidator<GetBasketByIdQuery>
{
    public GetBasketByIdQueryValidator(IBasketRepository basketRepository, IExecutionContextAccessor executionContextAccessor)
    {
        RuleFor(r => r.BasketId)
            .NotNull();

        RuleFor(r => r.BasketId)
        .MustAsync(async (id, _) =>
        {
                Basket? basket = await basketRepository.GetByIdAsync(BasketId.Create(id));

                if (basket is null)
                {
                return false;
            }

                if (basket.CustomerId != executionContextAccessor.UserId)
                {
                    return false;
                }

                return true;
            }).WithMessage("Basket was not found or user is not authorized to make the action");
    }
}
