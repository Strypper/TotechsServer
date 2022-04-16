using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class Update_ProjectInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppStoreLink",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GooglePlayLink",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MicrosoftStoreLink",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectBackground",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectLogo",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppStoreLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GooglePlayLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "MicrosoftStoreLink",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectBackground",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectLogo",
                table: "Projects");
        }
    }
}
