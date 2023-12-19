namespace UserAccess.Infrastructure.Authentication;

public enum Permission
{
    AccessUsers = 1,

    ReadMember = 2,

    ChangeUser = 3,

    RemoveUser = 4,

    DeleteComment = 5,

    GetComments = 6,

    UpdateComment = 7,

    AddComment = 8,

    GetProducts = 9,

    ModifyProduct = 10,

    RemoveProduct = 11,

    GetSales = 12,

    GetItems = 13,

    GetOrders = 14,

    GetBasket = 15,

    GetBuys = 16,

    CancelBuy = 17,

    BuyItem = 18,

    AddItemToBasket = 20,

    DeleteBasketItem = 21,

    BuyBasket = 22,

    PublishProduct = 23,

    GetRatings = 24,

    AddRating = 25,

    UpdateRating = 26,

    DeleteRating = 27,

    SaleProduct = 28,

    UpdateProduct = 29,
}
