using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddShoppingContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "shopping");

            migrationBuilder.CreateTable(
                name: "baskets",
                schema: "shopping",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfProducts = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_baskets", x => x.BasketId);
                });

            migrationBuilder.CreateTable(
                name: "Buys",
                schema: "shopping",
                columns: table => new
                {
                    BuyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfProduct = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal (18,2)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OcurredOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buys", x => x.BuyId);
                });

            migrationBuilder.CreateTable(
                name: "ItemId",
                columns: table => new
                {
                    Value = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemId", x => x.Value);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "shopping",
                columns: table => new
                {
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SellerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InStock = table.Column<int>(type: "int", nullable: false),
                    StockStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "shopping",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AmountOfItems = table.Column<int>(type: "int", nullable: false),
                    TotalMoneyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ConfirmedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpireOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PayedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Payments",
                schema: "shopping",
                columns: table => new
                {
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MoneyAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.PaymentId);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingOutboxMessages",
                schema: "shopping",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OcurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingOutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Wishes",
                schema: "shopping",
                columns: table => new
                {
                    WishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsPrivate = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishes", x => x.WishId);
                });

            migrationBuilder.CreateTable(
                name: "BasketItems",
                schema: "shopping",
                columns: table => new
                {
                    BasketId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BasketId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemIdsValue = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketItems", x => new { x.BasketId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_BasketItems_ItemId_ItemIdsValue",
                        column: x => x.ItemIdsValue,
                        principalTable: "ItemId",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BasketItems_baskets_BasketId1",
                        column: x => x.BasketId1,
                        principalSchema: "shopping",
                        principalTable: "baskets",
                        principalColumn: "BasketId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishItems",
                schema: "shopping",
                columns: table => new
                {
                    WishId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ItemsValue = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WishId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishItems", x => new { x.WishId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_WishItems_ItemId_ItemsValue",
                        column: x => x.ItemsValue,
                        principalTable: "ItemId",
                        principalColumn: "Value",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishItems_Wishes_WishId1",
                        column: x => x.WishId1,
                        principalSchema: "shopping",
                        principalTable: "Wishes",
                        principalColumn: "WishId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_BasketId1",
                schema: "shopping",
                table: "BasketItems",
                column: "BasketId1");

            migrationBuilder.CreateIndex(
                name: "IX_BasketItems_ItemIdsValue",
                schema: "shopping",
                table: "BasketItems",
                column: "ItemIdsValue");

            migrationBuilder.CreateIndex(
                name: "IX_WishItems_ItemsValue",
                schema: "shopping",
                table: "WishItems",
                column: "ItemsValue");

            migrationBuilder.CreateIndex(
                name: "IX_WishItems_WishId1",
                schema: "shopping",
                table: "WishItems",
                column: "WishId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketItems",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "Buys",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "Payments",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "ShoppingOutboxMessages",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "WishItems",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "baskets",
                schema: "shopping");

            migrationBuilder.DropTable(
                name: "ItemId");

            migrationBuilder.DropTable(
                name: "Wishes",
                schema: "shopping");
        }
    }
}
