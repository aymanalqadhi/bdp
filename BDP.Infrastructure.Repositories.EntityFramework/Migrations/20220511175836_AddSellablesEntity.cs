using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddSellablesEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Sellable_SellableId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Sellable_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellable_Users_OfferedById",
                table: "Sellable");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Sellable_ServiceId",
                table: "ServiceReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellable",
                table: "Sellable");

            migrationBuilder.RenameTable(
                name: "Sellable",
                newName: "Sellables");

            migrationBuilder.RenameIndex(
                name: "IX_Sellable_OfferedById",
                table: "Sellables",
                newName: "IX_Sellables_OfferedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellables",
                table: "Sellables",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Sellables_SellableId",
                table: "Attachments",
                column: "SellableId",
                principalTable: "Sellables",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Sellables_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellables_Users_OfferedById",
                table: "Sellables",
                column: "OfferedById",
                principalTable: "Users",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Sellables_SellableId",
                table: "Attachments");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductOrders_Sellables_ProductId",
                table: "ProductOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_Sellables_Users_OfferedById",
                table: "Sellables");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Sellables_ServiceId",
                table: "ServiceReservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sellables",
                table: "Sellables");

            migrationBuilder.RenameTable(
                name: "Sellables",
                newName: "Sellable");

            migrationBuilder.RenameIndex(
                name: "IX_Sellables_OfferedById",
                table: "Sellable",
                newName: "IX_Sellable_OfferedById");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sellable",
                table: "Sellable",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Sellable_SellableId",
                table: "Attachments",
                column: "SellableId",
                principalTable: "Sellable",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductOrders_Sellable_ProductId",
                table: "ProductOrders",
                column: "ProductId",
                principalTable: "Sellable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sellable_Users_OfferedById",
                table: "Sellable",
                column: "OfferedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Sellable_ServiceId",
                table: "ServiceReservations",
                column: "ServiceId",
                principalTable: "Sellable",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
