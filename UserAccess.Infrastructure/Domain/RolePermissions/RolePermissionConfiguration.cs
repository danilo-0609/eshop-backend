using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Enums;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.RolePermissions;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    private readonly DbContextOptionsBuilder<UserAccessDbContext> _dbContextOptionsBuilder;

    public RolePermissionConfiguration(DbContextOptionsBuilder<UserAccessDbContext> dbContextOptionsBuilder)
    {
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
    }

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolePermissions", "users");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Administrator, Permissions.ReadUser),
            Create(Role.Administrator, Permissions.AccessUsers),
            Create(Role.Administrator, Permissions.ChangeUser),
            Create(Role.Administrator, Permissions.RemoveUser),
            Create(Role.Administrator, Permissions.DeleteComment),
            Create(Role.Administrator, Permissions.GetUserRegistration),
            Create(Role.Administrator, Permissions.GetComments),
            Create(Role.Administrator, Permissions.UpdateComment),
            Create(Role.Administrator, Permissions.AddComment),
            Create(Role.Administrator, Permissions.GetRatings),
            Create(Role.Administrator, Permissions.DeleteRating),
            Create(Role.Administrator, Permissions.GetProducts),
            Create(Role.Administrator, Permissions.ModifyProduct),
            Create(Role.Administrator, Permissions.RemoveProduct),
            Create(Role.Administrator, Permissions.GetSales),
            Create(Role.Administrator, Permissions.GetItems),
            Create(Role.Administrator, Permissions.GetOrders),
            Create(Role.Administrator, Permissions.GetBasket),
            Create(Role.Administrator, Permissions.GetBuys),
            Create(Role.Administrator, Permissions.CancelBuy),
            Create(Role.Customer, Permissions.ReadUser),
            Create(Role.Customer, Permissions.ChangeUser),
            Create(Role.Customer, Permissions.RemoveUser),
            Create(Role.Customer, Permissions.ReadUser),
            Create(Role.Customer, Permissions.DeleteComment),
            Create(Role.Customer, Permissions.GetComments),
            Create(Role.Customer, Permissions.UpdateComment),
            Create(Role.Customer, Permissions.AddComment),
            Create(Role.Customer, Permissions.GetProducts),
            Create(Role.Customer, Permissions.GetRatings),
            Create(Role.Customer, Permissions.AddRating),
            Create(Role.Customer, Permissions.UpdateRating),
            Create(Role.Customer, Permissions.DeleteRating),
            Create(Role.Customer, Permissions.PublishProduct),
            Create(Role.Customer, Permissions.GetBasket),
            Create(Role.Customer, Permissions.GetItems),
            Create(Role.Customer, Permissions.GetOrders),
            Create(Role.Customer, Permissions.GetBuys),
            Create(Role.Customer, Permissions.BuyItem),
            Create(Role.Customer, Permissions.BuyBasket),
            Create(Role.Customer, Permissions.CancelBuy),
            Create(Role.Customer, Permissions.AddItemInBasket),
            Create(Role.Customer, Permissions.DeleteBasketItem),
            Create(Role.Customer, Permissions.BuyBasket),
            Create(Role.Seller, Permissions.ReadUser),
            Create(Role.Seller, Permissions.ChangeUser),
            Create(Role.Seller, Permissions.RemoveUser),
            Create(Role.Seller, Permissions.DeleteComment),
            Create(Role.Seller, Permissions.GetComments),
            Create(Role.Seller, Permissions.UpdateComment),
            Create(Role.Seller, Permissions.AddComment),
            Create(Role.Seller, Permissions.GetRatings),
            Create(Role.Seller, Permissions.GetSales),
            Create(Role.Seller, Permissions.SaleProduct),
            Create(Role.Seller, Permissions.PublishProduct),
            Create(Role.Seller, Permissions.UpdateProduct),
            Create(Role.Seller, Permissions.RemoveProduct),
            Create(Role.Seller, Permissions.GetOrders));

        _dbContextOptionsBuilder.EnableSensitiveDataLogging();
    }

    private static RolePermission Create(Role role, Permissions permissions)
    {
        return new RolePermission(
            role.Id,
            (int)permissions);
    }
}
