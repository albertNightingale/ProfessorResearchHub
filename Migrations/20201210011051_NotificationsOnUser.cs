using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class NotificationsOnUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_URCUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_URCUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "URCUserId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "isRead",
                table: "Notification",
                newName: "IsRead");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_AspNetUsers_UserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_UserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notification");

            migrationBuilder.RenameColumn(
                name: "IsRead",
                table: "Notification",
                newName: "isRead");

            migrationBuilder.AddColumn<string>(
                name: "URCUserId",
                table: "Notification",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_URCUserId",
                table: "Notification",
                column: "URCUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_AspNetUsers_URCUserId",
                table: "Notification",
                column: "URCUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
