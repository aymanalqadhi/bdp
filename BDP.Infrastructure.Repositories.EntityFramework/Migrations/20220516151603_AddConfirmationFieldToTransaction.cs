using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddConfirmationFieldToTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionConfirmations_TransactionId",
                table: "TransactionConfirmations");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionConfirmations_TransactionId",
                table: "TransactionConfirmations",
                column: "TransactionId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TransactionConfirmations_TransactionId",
                table: "TransactionConfirmations");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionConfirmations_TransactionId",
                table: "TransactionConfirmations",
                column: "TransactionId");
        }
    }
}
