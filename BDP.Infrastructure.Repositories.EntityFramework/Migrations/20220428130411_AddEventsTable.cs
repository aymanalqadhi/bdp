using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddEventsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Sellable",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakesPlaceAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sellable_EventId",
                table: "Sellable",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sellable_Events_EventId",
                table: "Sellable",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellable_Events_EventId",
                table: "Sellable");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Sellable_EventId",
                table: "Sellable");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Sellable");
        }
    }
}