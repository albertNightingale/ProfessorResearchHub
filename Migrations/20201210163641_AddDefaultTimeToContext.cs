using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class AddDefaultTimeToContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTime",
                table: "Notifications",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedTime",
                table: "Notifications");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notifications",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
