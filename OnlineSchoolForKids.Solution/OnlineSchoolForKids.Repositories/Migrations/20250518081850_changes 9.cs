using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class changes9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiddenModules_AspNetUsers_UserId",
                table: "HiddenModules");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "HiddenModules",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_HiddenModules_UserId",
                table: "HiddenModules",
                newName: "IX_HiddenModules_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiddenModules_AspNetUsers_ApplicationUserId",
                table: "HiddenModules",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HiddenModules_AspNetUsers_ApplicationUserId",
                table: "HiddenModules");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "HiddenModules",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_HiddenModules_ApplicationUserId",
                table: "HiddenModules",
                newName: "IX_HiddenModules_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_HiddenModules_AspNetUsers_UserId",
                table: "HiddenModules",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
