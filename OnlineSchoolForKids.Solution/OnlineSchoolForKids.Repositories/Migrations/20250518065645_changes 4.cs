using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class changes4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TypeAr",
                table: "Contents");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TypeAr",
                table: "Contents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
