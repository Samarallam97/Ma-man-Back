using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Contentadminrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Content_AdminId",
                table: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Content_AdminId",
                table: "Content",
                column: "AdminId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Content_AdminId",
                table: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Content_AdminId",
                table: "Content",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");
        }
    }
}
