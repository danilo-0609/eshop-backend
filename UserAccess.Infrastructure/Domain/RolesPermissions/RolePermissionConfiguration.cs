using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserAccess.Domain.Users;

namespace UserAccess.Infrastructure.Domain.RolesPermissions;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    private readonly DbContextOptionsBuilder _dbContextOptionsBuilder;

    public RolePermissionConfiguration(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        _dbContextOptionsBuilder = dbContextOptionsBuilder;
    }

    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable("RolesPermissions", "users");

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.Property(x => x.RoleId);

        builder.Property(x => x.PermissionId);

        builder.HasData(new RolePermission[]
        {
            // Administrator
            new RolePermission { RoleId = 1, PermissionId = 2 }, // ReadUser
            new RolePermission { RoleId = 1, PermissionId = 1 }, // AccessUsers
            new RolePermission { RoleId = 1, PermissionId = 3 }, // ChangeUser
            new RolePermission { RoleId = 1, PermissionId = 4 }, // RemoveUser
            new RolePermission { RoleId = 1, PermissionId = 5 }, // DeleteComment
            new RolePermission { RoleId = 1, PermissionId = 29 }, // GetUserRegistration
            new RolePermission { RoleId = 1, PermissionId = 6 }, // GetComments
            new RolePermission { RoleId = 1, PermissionId = 7 }, // UpdateComment
            new RolePermission { RoleId = 1, PermissionId = 8 }, // AddComment
            new RolePermission { RoleId = 1, PermissionId = 23 }, // GetRatings
            new RolePermission { RoleId = 1, PermissionId = 26 }, // DeleteRating
            new RolePermission { RoleId = 1, PermissionId = 9 }, // GetProducts
            new RolePermission { RoleId = 1, PermissionId = 10 }, // ModifyProduct
            new RolePermission { RoleId = 1, PermissionId = 11 }, // RemoveProduct
            new RolePermission { RoleId = 1, PermissionId = 12 }, // GetSales
            new RolePermission { RoleId = 1, PermissionId = 13 }, // GetItems
            new RolePermission { RoleId = 1, PermissionId = 14 }, // GetOrders
            new RolePermission { RoleId = 1, PermissionId = 15 }, // GetBasket
            new RolePermission { RoleId = 1, PermissionId = 16 }, // GetBuys
            new RolePermission { RoleId = 1, PermissionId = 17 }, // CancelBuy

            // Customer
            new RolePermission { RoleId = 2, PermissionId = 2 }, // ReadUser
            new RolePermission { RoleId = 2, PermissionId = 3 }, // ChangeUser
            new RolePermission { RoleId = 2, PermissionId = 4 }, // RemoveUser
            new RolePermission { RoleId = 2, PermissionId = 2 }, // ReadUser (duplicate entry)
            new RolePermission { RoleId = 2, PermissionId = 5 }, // DeleteComment
            new RolePermission { RoleId = 2, PermissionId = 6 }, // GetComments
            new RolePermission { RoleId = 2, PermissionId = 7 }, // UpdateComment
            new RolePermission { RoleId = 2, PermissionId = 8 }, // AddComment
            new RolePermission { RoleId = 2, PermissionId = 9 }, // GetProducts
            new RolePermission { RoleId = 2, PermissionId = 23 }, // GetRatings
            new RolePermission { RoleId = 2, PermissionId = 24 }, // AddRating
            new RolePermission { RoleId = 2, PermissionId = 25 }, // UpdateRating
            new RolePermission { RoleId = 2, PermissionId = 26 }, // DeleteRating
            new RolePermission { RoleId = 2, PermissionId = 22 }, // PublishProduct
            new RolePermission { RoleId = 2, PermissionId = 15 }, // GetBasket
            new RolePermission { RoleId = 2, PermissionId = 13 }, // GetItems
            new RolePermission { RoleId = 2, PermissionId = 14 }, // GetOrders
            new RolePermission { RoleId = 2, PermissionId = 16 }, // GetBuys
            new RolePermission { RoleId = 2, PermissionId = 18 }, // BuyItem
            new RolePermission { RoleId = 2, PermissionId = 21 }, // BuyBasket
            new RolePermission { RoleId = 2, PermissionId = 17 }, // CancelBuy
            new RolePermission { RoleId = 2, PermissionId = 19 }, // AddItemInBasket
            new RolePermission { RoleId = 2, PermissionId = 20 }, // DeleteBasketItem

            // Seller
            new RolePermission { RoleId = 3, PermissionId = 2 }, // ReadUser
            new RolePermission { RoleId = 3, PermissionId = 3 }, // ChangeUser
            new RolePermission { RoleId = 3, PermissionId = 4 }, // RemoveUser
            new RolePermission { RoleId = 3, PermissionId = 5 }, // DeleteComment
            new RolePermission { RoleId = 3, PermissionId = 6 }, // GetComments
            new RolePermission { RoleId = 3, PermissionId = 7 }, // UpdateComment
            new RolePermission { RoleId = 3, PermissionId = 8 }, // AddComment
            new RolePermission { RoleId = 3, PermissionId = 23 }, // GetRatings
            new RolePermission { RoleId = 3, PermissionId = 12 }, // GetSales
            new RolePermission { RoleId = 3, PermissionId = 27 }, // SaleProduct
            new RolePermission { RoleId = 3, PermissionId = 22 }, // PublishProduct
            new RolePermission { RoleId = 3, PermissionId = 28 }, // UpdateProduct
            new RolePermission { RoleId = 3, PermissionId = 11 }, // RemoveProduct
            new RolePermission { RoleId = 3, PermissionId = 14 }  // GetOrders
        });

        _dbContextOptionsBuilder.EnableSensitiveDataLogging();

    }
}
