using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AgeGroups_AgeGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AgeGroupId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AgeGroupId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AgeGroupId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AgeGroupId",
                table: "AspNetUsers",
                column: "AgeGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AgeGroups_AgeGroupId",
                table: "AspNetUsers",
                column: "AgeGroupId",
                principalTable: "AgeGroups",
                principalColumn: "Id");
        }
    }
}
