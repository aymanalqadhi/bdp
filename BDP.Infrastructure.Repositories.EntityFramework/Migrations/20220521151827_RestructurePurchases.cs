using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class RestructurePurchases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Sellables_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Sellables_ServiceId",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ServiceReservations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "ProductOrders");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "ServiceReservations",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReservations_ServiceId",
                table: "ServiceReservations",
                newName: "IX_ServiceReservations_ItemId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ProductOrders",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Sellables_ItemId",
                table: "ProductOrders",
                column: "ItemId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Sellables_ItemId",
                table: "ServiceReservations",
                column: "ItemId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Sellables_ItemId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Sellables_ItemId",
                table: "ServiceReservations");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ServiceReservations",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReservations_ItemId",
                table: "ServiceReservations",
                newName: "IX_ServiceReservations_ServiceId");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "ProductOrders",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductOrders_ItemId",
                table: "ProductOrders",
                newName: "IX_ProductOrders_ProductId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ServiceReservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "ProductOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Sellables_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Sellables_ServiceId",
                table: "ServiceReservations",
                column: "ServiceId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}