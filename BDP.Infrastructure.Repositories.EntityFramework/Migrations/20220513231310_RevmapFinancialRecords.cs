using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class RevmapFinancialRecords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialRecords_Attachments_DocumentId",
                table: "FinancialRecords");

            migrationBuilder.DropIndex(
                name: "IX_FinancialRecords_DocumentId",
                table: "FinancialRecords");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "FinancialRecords");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "FinancialRecordVerifications",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "FinancialRecordVerifications",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecordVerifications_DocumentId",
                table: "FinancialRecordVerifications",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecordVerifications_Attachments_DocumentId",
                table: "FinancialRecordVerifications",
                column: "DocumentId",
                principalTable: "Attachments",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FinancialRecordVerifications_Attachments_DocumentId",
                table: "FinancialRecordVerifications");

            migrationBuilder.DropIndex(
                name: "IX_FinancialRecordVerifications_DocumentId",
                table: "FinancialRecordVerifications");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "FinancialRecordVerifications");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "FinancialRecordVerifications");

            migrationBuilder.AddColumn<long>(
                name: "DocumentId",
                table: "FinancialRecords",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_FinancialRecords_DocumentId",
                table: "FinancialRecords",
                column: "DocumentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FinancialRecords_Attachments_DocumentId",
                table: "FinancialRecords",
                column: "DocumentId",
                principalTable: "Attachments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
