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
    }
}
