using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddCoverPictureFieldToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CoverPictureId",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastLogin",
                table: "RefreshTokens",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Users_CoverPictureId",
                table: "Users",
                column: "CoverPictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Attachments_CoverPictureId",
                table: "Users",
                column: "CoverPictureId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Attachments_CoverPictureId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CoverPictureId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CoverPictureId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LastLogin",
                table: "RefreshTokens");
        }
    }
}
