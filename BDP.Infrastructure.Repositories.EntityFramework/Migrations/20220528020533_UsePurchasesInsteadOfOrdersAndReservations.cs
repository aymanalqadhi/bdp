using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class UsePurchasesInsteadOfOrdersAndReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Events_ServiceReservation_EventId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ServiceReservation_EventId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ServiceReservation_EventId",
                table: "Purchases");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ServiceReservation_EventId",
                table: "Purchases",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ServiceReservation_EventId",
                table: "Purchases",
                column: "ServiceReservation_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Events_ServiceReservation_EventId",
                table: "Purchases",
                column: "ServiceReservation_EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
