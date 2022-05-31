using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class ReplaceSellablesWithOrdersAndReseravationsEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sellable_Events_EventId",
                table: "Sellable");

            migrationBuilder.DropIndex(
                name: "IX_Sellable_EventId",
                table: "Sellable");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Sellable");

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "ServiceReservations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "ProductOrders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReservations_EventId",
                table: "ServiceReservations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_EventId",
                table: "ProductOrders",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Events_EventId",
                table: "ProductOrders",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Events_EventId",
                table: "ServiceReservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Events_EventId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Events_EventId",
                table: "ServiceReservations");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReservations_EventId",
                table: "ServiceReservations");

            migrationBuilder.DropIndex(
                name: "IX_ProductOrders_EventId",
                table: "ProductOrders");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "ProductOrders");

            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "Sellable",
                type: "bigint",
                nullable: true);

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
    }
}