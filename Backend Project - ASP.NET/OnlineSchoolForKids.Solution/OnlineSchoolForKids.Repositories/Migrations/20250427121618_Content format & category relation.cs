using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class Contentformatcategoryrelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Content_CategoryId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_FormatId",
                table: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CategoryId",
                table: "Content",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Content_FormatId",
                table: "Content",
                column: "FormatId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Content_CategoryId",
                table: "Content");

            migrationBuilder.DropIndex(
                name: "IX_Content_FormatId",
                table: "Content");

            migrationBuilder.CreateIndex(
                name: "IX_Content_CategoryId",
                table: "Content",
                column: "CategoryId",
                unique: true,
                filter: "[CategoryId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Content_FormatId",
                table: "Content",
                column: "FormatId",
                unique: true,
                filter: "[FormatId] IS NOT NULL");
        }
    }
}
