using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class addNotificationsCollectionToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NotificationMessage = table.Column<string>(nullable: true),
                    IsRead = table.Column<bool>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    URCUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notification_AspNetUsers_URCUserId",
                        column: x => x.URCUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_URCUserId",
                table: "Notification",
                column: "URCUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notification");
        }
    }
}
