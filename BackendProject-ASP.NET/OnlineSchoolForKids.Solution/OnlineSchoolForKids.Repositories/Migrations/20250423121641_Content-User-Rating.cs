using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class ContentUserRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AverageRate",
                table: "Content",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ContentUserRating",
                columns: table => new
                {
                    Content1Id = table.Column<int>(type: "int", nullable: false),
                    User1Id = table.Column<int>(type: "int", nullable: false),
					Stars = table.Column<int>(type: "int", nullable: false)
				},
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentUserRating", x => new { x.Content1Id, x.User1Id });
                    table.ForeignKey(
                        name: "FK_ContentUserRating_Content_Content1Id",
                        column: x => x.Content1Id,
                        principalTable: "Content",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentUserRating_Users_User1Id",
                        column: x => x.User1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentUserRating_User1Id",
                table: "ContentUserRating",
                column: "User1Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentUserRating");

            migrationBuilder.DropColumn(
                name: "AverageRate",
                table: "Content");
        }
    }
}
