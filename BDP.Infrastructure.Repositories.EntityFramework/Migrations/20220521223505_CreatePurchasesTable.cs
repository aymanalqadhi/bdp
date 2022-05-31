using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class CreatePurchasesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Events_EventId",
                table: "ServiceReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Sellables_ServiceId",
                table: "ServiceReservations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReservations_Transactions_TransactionId",
                table: "ServiceReservations");

            migrationBuilder.DropTable(
                name: "ProductOrders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceReservations",
                table: "ServiceReservations");

            migrationBuilder.RenameTable(
                name: "ServiceReservations",
                newName: "Purchases");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReservations_TransactionId",
                table: "Purchases",
                newName: "IX_Purchases_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReservations_ServiceId",
                table: "Purchases",
                newName: "IX_Purchases_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReservations_EventId",
                table: "Purchases",
                newName: "IX_Purchases_EventId");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "Purchases",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "Purchases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "ProductId",
                table: "Purchases",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Quantity",
                table: "Purchases",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ServiceReservation_EventId",
                table: "Purchases",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Purchases_ServiceReservation_EventId",
                table: "Purchases",
                column: "ServiceReservation_EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Events_EventId",
                table: "Purchases",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Events_ServiceReservation_EventId",
                table: "Purchases",
                column: "ServiceReservation_EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Sellables_ProductId",
                table: "Purchases",
                column: "ProductId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Sellables_ServiceId",
                table: "Purchases",
                column: "ServiceId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                table: "Purchases",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Events_EventId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Events_ServiceReservation_EventId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Sellables_ProductId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Sellables_ServiceId",
                table: "Purchases");

            migrationBuilder.DropForeignKey(
                name: "FK_Purchases_Transactions_TransactionId",
                table: "Purchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Purchases",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ProductId",
                table: "Purchases");

            migrationBuilder.DropIndex(
                name: "IX_Purchases_ServiceReservation_EventId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Purchases");

            migrationBuilder.DropColumn(
                name: "ServiceReservation_EventId",
                table: "Purchases");

            migrationBuilder.RenameTable(
                name: "Purchases",
                newName: "ServiceReservations");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_TransactionId",
                table: "ServiceReservations",
                newName: "IX_ServiceReservations_TransactionId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_ServiceId",
                table: "ServiceReservations",
                newName: "IX_ServiceReservations_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Purchases_EventId",
                table: "ServiceReservations",
                newName: "IX_ServiceReservations_EventId");

            migrationBuilder.AlterColumn<long>(
                name: "ServiceId",
                table: "ServiceReservations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceReservations",
                table: "ServiceReservations",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ProductOrders",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<long>(type: "bigint", nullable: false),
                    TransactionId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EventId = table.Column<long>(type: "bigint", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductOrders_Sellables_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Sellables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductOrders_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_EventId",
                table: "ProductOrders",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_ProductId",
                table: "ProductOrders",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductOrders_TransactionId",
                table: "ProductOrders",
                column: "TransactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Events_EventId",
                table: "ServiceReservations",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Sellables_ServiceId",
                table: "ServiceReservations",
                column: "ServiceId",
                principalTable: "Sellables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReservations_Transactions_TransactionId",
                table: "ServiceReservations",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}