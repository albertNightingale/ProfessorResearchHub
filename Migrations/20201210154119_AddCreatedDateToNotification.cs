using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class AddCreatedDateToNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "GraduationDate",
                table: "URCUserProfile",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Notifications",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Notifications");

            migrationBuilder.AlterColumn<string>(
                name: "GraduationDate",
                table: "URCUserProfile",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime));
        }
    }
}
