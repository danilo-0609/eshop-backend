using BuildingBlocks.Domain;
using ErrorOr;
using Shopping.Domain.Orders.Errors;

namespace Shopping.Domain.Orders.Rules;

internal sealed class OrderCannotBePlacedByItemSellerRule : IBusinessRule
{
    private readonly Guid _customerId;
    private readonly Guid _sellerId;

    public OrderCannotBePlacedByItemSellerRule(Guid customerId, Guid sellerId)
    {
        _customerId = customerId;
        _sellerId = sellerId;
    }

    public Error Error => OrderErrorCodes.CannotBePlacedBySeller;

    public bool IsBroken() => _customerId == _sellerId;

    public static string Message => "Order cannot be placed by the same item seller";
}
