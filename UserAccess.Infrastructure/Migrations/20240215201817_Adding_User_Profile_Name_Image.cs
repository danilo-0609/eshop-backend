using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingUserProfileNameImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileImageName",
                schema: "users",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileImageName",
                schema: "users",
                table: "Users");
        }
    }
}
