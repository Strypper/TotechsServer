using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Intranet.Entities.Migrations
{
    public partial class Conversation_AddName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Conversations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Conversations");
        }
    }
}
