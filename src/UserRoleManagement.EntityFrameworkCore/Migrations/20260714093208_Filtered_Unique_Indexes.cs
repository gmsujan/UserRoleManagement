using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserRoleManagement.Migrations
{
    /// <inheritdoc />
    public partial class Filtered_Unique_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppAppRoles_Name",
                table: "AppAppRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AppAppRoles_Name",
                table: "AppAppRoles",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AppAppRoles_Name",
                table: "AppAppRoles");

            migrationBuilder.CreateIndex(
                name: "IX_AppAppRoles_Name",
                table: "AppAppRoles",
                column: "Name",
                unique: true);
        }
    }
}
