using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class Test_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConversationId1",
                table: "UserConversations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                table: "UserConversations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_ConversationId1",
                table: "UserConversations",
                column: "ConversationId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserConversations_UserId1",
                table: "UserConversations",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId1",
                table: "UserConversations",
                column: "ConversationId1",
                principalTable: "Conversations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserConversations_Users_UserId1",
                table: "UserConversations",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Conversations_ConversationId1",
                table: "UserConversations");

            migrationBuilder.DropForeignKey(
                name: "FK_UserConversations_Users_UserId1",
                table: "UserConversations");

            migrationBuilder.DropIndex(
                name: "IX_UserConversations_ConversationId1",
                table: "UserConversations");

            migrationBuilder.DropIndex(
                name: "IX_UserConversations_UserId1",
                table: "UserConversations");

            migrationBuilder.DropColumn(
                name: "ConversationId1",
                table: "UserConversations");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "UserConversations");
        }
    }
}
