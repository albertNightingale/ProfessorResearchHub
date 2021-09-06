using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class removeStudentID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Notification");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Notification",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
