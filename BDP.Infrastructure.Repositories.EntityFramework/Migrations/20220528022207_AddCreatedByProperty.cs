using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddCreatedByProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CreatedById",
                table: "Events",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedById",
                table: "Events",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Users_CreatedById",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_CreatedById",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Events");
        }
    }
}