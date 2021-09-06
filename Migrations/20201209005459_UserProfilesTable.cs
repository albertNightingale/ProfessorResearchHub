using Microsoft.EntityFrameworkCore.Migrations;

namespace URC.Migrations
{
    public partial class UserProfilesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "URCUserProfile",
                columns: table => new
                {
                    URCUserProfileID = table.Column<string>(nullable: false),
                    Skills = table.Column<string>(nullable: true),
                    Resume = table.Column<string>(nullable: true),
                    Interests = table.Column<string>(nullable: true),
                    GraduationDate = table.Column<string>(nullable: true),
                    CompletedCourses = table.Column<string>(nullable: true),
                    DegreePlan = table.Column<string>(nullable: true),
                    UID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    GPA = table.Column<double>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_URCUserProfile", x => x.URCUserProfileID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "URCUserProfile");
        }
    }
}
