using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddProgressToEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Progress",
                table: "Events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[] { 6L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DropColumn(
                name: "Progress",
                table: "Events");
        }
    }
}