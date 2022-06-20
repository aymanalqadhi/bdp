using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class AddSellableReviewsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SellableReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemId = table.Column<long>(type: "bigint", nullable: false),
                    LeftById = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellableReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SellableReviews_Sellables_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Sellables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SellableReviews_Users_LeftById",
                        column: x => x.LeftById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SellableReviews_ItemId",
                table: "SellableReviews",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_SellableReviews_LeftById",
                table: "SellableReviews",
                column: "LeftById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SellableReviews");
        }
    }
}
