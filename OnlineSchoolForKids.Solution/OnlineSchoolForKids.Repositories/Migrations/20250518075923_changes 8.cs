using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineSchoolForKids.Repositories.Migrations
{
    /// <inheritdoc />
    public partial class changes8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "LastUpdateDate",
                table: "Diaries",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "Diaries");
        }
    }
}
