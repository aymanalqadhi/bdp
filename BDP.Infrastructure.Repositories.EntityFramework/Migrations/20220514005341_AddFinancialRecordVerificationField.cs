using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddFinancialRecordVerificationField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialRecordVerifications_FinancialRecordId",
                table: "FinancialRecordVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecordVerifications_FinancialRecordId",
                table: "FinancialRecordVerifications",
                column: "FinancialRecordId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FinancialRecordVerifications_FinancialRecordId",
                table: "FinancialRecordVerifications");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecordVerifications_FinancialRecordId",
                table: "FinancialRecordVerifications",
                column: "FinancialRecordId");
        }
    }
}