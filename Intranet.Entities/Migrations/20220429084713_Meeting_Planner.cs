using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class Meeting_Planner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PlannerId",
                table: "MeetingSchedules",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MeetingSchedules_PlannerId",
                table: "MeetingSchedules",
                column: "PlannerId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingSchedules_Users_PlannerId",
                table: "MeetingSchedules",
                column: "PlannerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeetingSchedules_Users_PlannerId",
                table: "MeetingSchedules");

            migrationBuilder.DropIndex(
                name: "IX_MeetingSchedules_PlannerId",
                table: "MeetingSchedules");

            migrationBuilder.DropColumn(
                name: "PlannerId",
                table: "MeetingSchedules");
        }
    }
}
