using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HostelListing.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "48e70493-426f-48a1-833e-5b1a3ee2df4c", "c6c820d0-50aa-491f-b6b0-e5840ccb546e", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6c13c588-a74d-42df-8ae2-9de2b8ca502b", "ea23a397-08ed-4b89-ba72-ff3df748d9d5", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "48e70493-426f-48a1-833e-5b1a3ee2df4c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6c13c588-a74d-42df-8ae2-9de2b8ca502b");
        }
    }
}
