using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class RemoveRestrictProps : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<int>(
                name: "GroupChatId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GroupChat",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupColor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupChat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ChatMessage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    GroupId = table.Column<int>(type: "int", nullable: false),
                    MessageContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatMessage_GroupChat_GroupId",
                        column: x => x.GroupId,
                        principalTable: "GroupChat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMessage_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_GroupChatId",
                table: "Users",
                column: "GroupChatId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_GroupId",
                table: "ChatMessage",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMessage_UserId",
                table: "ChatMessage",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_GroupChat_GroupChatId",
                table: "Users",
                column: "GroupChatId",
                principalTable: "GroupChat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_GroupChat_GroupChatId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "ChatMessage");

            migrationBuilder.DropTable(
                name: "GroupChat");

            migrationBuilder.DropIndex(
                name: "IX_Users_GroupChatId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GroupChatId",
                table: "Users");

            migrationBuilder.AlterColumn<bool>(
                name: "Gender",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }
    }
}
