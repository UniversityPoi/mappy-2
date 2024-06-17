using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mappyserver.Migrations
{
    public partial class AddedUpdateUserMethod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastReportedAccidentDate",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastReportedAccidentDate",
                table: "Users");
        }
    }
}
