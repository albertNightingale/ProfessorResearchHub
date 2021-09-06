using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class addingNotificationsToUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Notification",
                newName: "isRead");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRead",
                table: "Notification",
                newName: "IsRead");
        }
    }
}
