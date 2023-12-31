using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain;

namespace UserAccess.Infrastructure.Domain.Permissions;

internal sealed class PermissionConfiguration  //IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable("Permissions", "users");

        builder.HasKey(r => r.PermissionId);

        builder.Property(r => r.PermissionId).ValueGeneratedNever();

        builder.Property(r => r.Name).HasMaxLength(300);

        builder.HasData(new Permission[]
        {
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.AccessUsers, Name = UserAccess.Domain.Enums.Permissions.AccessUsers.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.ReadUser, Name = UserAccess.Domain.Enums.Permissions.ReadUser.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.ChangeUser, Name = UserAccess.Domain.Enums.Permissions.ChangeUser.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.RemoveUser, Name = UserAccess.Domain.Enums.Permissions.RemoveUser.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.DeleteComment, Name = UserAccess.Domain.Enums.Permissions.DeleteComment.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetComments, Name = UserAccess.Domain.Enums.Permissions.GetComments.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.UpdateComment, Name = UserAccess.Domain.Enums.Permissions.UpdateComment.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.AddComment, Name = UserAccess.Domain.Enums.Permissions.AddComment.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetProducts, Name = UserAccess.Domain.Enums.Permissions.GetProducts.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.ModifyProduct, Name = UserAccess.Domain.Enums.Permissions.ModifyProduct.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.RemoveProduct, Name = UserAccess.Domain.Enums.Permissions.RemoveProduct.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetSales, Name = UserAccess.Domain.Enums.Permissions.GetSales.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetItems, Name = UserAccess.Domain.Enums.Permissions.GetItems.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetOrders, Name = UserAccess.Domain.Enums.Permissions.GetOrders.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetBasket, Name = UserAccess.Domain.Enums.Permissions.GetBasket.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetBuys, Name = UserAccess.Domain.Enums.Permissions.GetBuys.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.CancelBuy, Name = UserAccess.Domain.Enums.Permissions.CancelBuy.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.BuyItem, Name = UserAccess.Domain.Enums.Permissions.BuyItem.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.AddItemInBasket, Name = UserAccess.Domain.Enums.Permissions.AddItemInBasket.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.DeleteBasketItem, Name = UserAccess.Domain.Enums.Permissions.DeleteBasketItem.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.BuyBasket, Name = UserAccess.Domain.Enums.Permissions.BuyBasket.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.PublishProduct, Name = UserAccess.Domain.Enums.Permissions.PublishProduct.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetRatings, Name = UserAccess.Domain.Enums.Permissions.GetRatings.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.AddRating, Name = UserAccess.Domain.Enums.Permissions.AddRating.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.UpdateRating, Name = UserAccess.Domain.Enums.Permissions.UpdateRating.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.DeleteRating, Name = UserAccess.Domain.Enums.Permissions.DeleteRating.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.SaleProduct, Name = UserAccess.Domain.Enums.Permissions.SaleProduct.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.UpdateProduct, Name = UserAccess.Domain.Enums.Permissions.UpdateProduct.ToString() },
            new Permission { PermissionId = (int)UserAccess.Domain.Enums.Permissions.GetUserRegistration, Name = UserAccess.Domain.Enums.Permissions.GetUserRegistration.ToString() }});
    }
}
