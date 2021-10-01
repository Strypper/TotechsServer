using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Intranet.Entities.Migrations
{
    public partial class UserRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "Relationship");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Users",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Relationship",
                table: "Users",
                newName: "Age");
        }
    }
}
