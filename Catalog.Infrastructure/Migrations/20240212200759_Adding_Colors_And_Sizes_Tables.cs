using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingColorsAndSizesTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Size",
                schema: "catalog",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Colors",
                schema: "catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sizes",
                schema: "catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StockStatus",
                schema: "catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Colors",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Sizes",
                schema: "catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockStatus",
                schema: "catalog",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                schema: "catalog",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Size",
                schema: "catalog",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
