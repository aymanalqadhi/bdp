using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BDP.Infrastructure.Repositories.EntityFramework.Migrations
{
    public partial class UpdateRelationshipsIdTypesToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("91a8997e-4227-4c13-ad29-1f3033b123e8"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("91dd79e8-d0d4-492e-8d67-3b454da0ba89"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("b2a73d49-d6b7-49b3-8b9b-f567ef98c609"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("cee1cf92-8bfa-452a-9f0c-4ade726633a3"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("d4a67ed7-60cf-4ac1-a61b-52e465bf9687"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("eee6c146-71cc-4705-9d83-2483297cb454"));

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("170fb84e-37c2-4b2e-b385-e0abbf95c256"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engagement Party" },
                    { new Guid("2f46669a-75f9-4eec-a5be-2f49697a50e3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graduation Ceremony" },
                    { new Guid("485f4dda-6d83-4159-bfdc-c039cc1ef5b8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wedding" },
                    { new Guid("4931f454-22f6-4407-a212-fa54a12302c2"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other" },
                    { new Guid("e11de04c-5ce4-4a75-87dd-faaddb62da56"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graduation Party" },
                    { new Guid("e408c1d0-7409-41d0-9964-09ee5d3424d6"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birth Day" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("170fb84e-37c2-4b2e-b385-e0abbf95c256"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("2f46669a-75f9-4eec-a5be-2f49697a50e3"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("485f4dda-6d83-4159-bfdc-c039cc1ef5b8"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("4931f454-22f6-4407-a212-fa54a12302c2"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e11de04c-5ce4-4a75-87dd-faaddb62da56"));

            migrationBuilder.DeleteData(
                table: "EventTypes",
                keyColumn: "Id",
                keyValue: new Guid("e408c1d0-7409-41d0-9964-09ee5d3424d6"));

            migrationBuilder.InsertData(
                table: "EventTypes",
                columns: new[] { "Id", "CreatedAt", "ModifiedAt", "Name" },
                values: new object[,]
                {
                    { new Guid("91a8997e-4227-4c13-ad29-1f3033b123e8"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Birth Day" },
                    { new Guid("91dd79e8-d0d4-492e-8d67-3b454da0ba89"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graduation Party" },
                    { new Guid("b2a73d49-d6b7-49b3-8b9b-f567ef98c609"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Engagement Party" },
                    { new Guid("cee1cf92-8bfa-452a-9f0c-4ade726633a3"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Other" },
                    { new Guid("d4a67ed7-60cf-4ac1-a61b-52e465bf9687"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Wedding" },
                    { new Guid("eee6c146-71cc-4705-9d83-2483297cb454"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Graduation Ceremony" }
                });
        }
    }
}
