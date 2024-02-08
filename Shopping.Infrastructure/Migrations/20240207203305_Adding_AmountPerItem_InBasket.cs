using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shopping.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingAmountPerItemInBasket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountPerItem",
                schema: "shopping",
                table: "BasketItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPerItem",
                schema: "shopping",
                table: "BasketItems");
        }
    }
}
