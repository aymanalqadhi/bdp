using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddEventPicturesRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Attachments",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attachments_EventId",
                table: "Attachments",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Events_EventId",
                table: "Attachments",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Events_EventId",
                table: "Attachments");

            migrationBuilder.DropIndex(
                name: "IX_Attachments_EventId",
                table: "Attachments");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Attachments");
        }
    }
}
