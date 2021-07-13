using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class Fixmemberprop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_Teams_MyPropertyId",
                table: "TeamUser");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "MyPropertyId",
                table: "TeamUser",
                newName: "TeamId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUser_MyPropertyId",
                table: "TeamUser",
                newName: "IX_TeamUser_TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_Teams_TeamId",
                table: "TeamUser",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeamUser_Teams_TeamId",
                table: "TeamUser");

            migrationBuilder.RenameColumn(
                name: "TeamId",
                table: "TeamUser",
                newName: "MyPropertyId");

            migrationBuilder.RenameIndex(
                name: "IX_TeamUser_TeamId",
                table: "TeamUser",
                newName: "IX_TeamUser_MyPropertyId");

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TeamUser_Teams_MyPropertyId",
                table: "TeamUser",
                column: "MyPropertyId",
                principalTable: "Teams",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
