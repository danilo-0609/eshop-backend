using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccess.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsersRolesFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateIndex(
                name: "IX_UsersRoles_UserId",
                schema: "users",
                table: "UsersRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UsersRoles_UserId",
                schema: "users",
                table: "UsersRoles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsersRoles",
                schema: "users",
                table: "UsersRoles",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}
