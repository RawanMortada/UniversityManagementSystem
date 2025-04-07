using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UniversityManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "182fea8b-62d1-4f89-9ca6-ea92294cfbc4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7264453e-a3d4-4495-8d46-0f8ff38f616b");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02dfce41-35c7-400a-84e5-dab3e4ecf10c", null, "User", "USER" },
                    { "81d663da-41e7-4dee-ab0a-5753ca2769f6", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02dfce41-35c7-400a-84e5-dab3e4ecf10c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "81d663da-41e7-4dee-ab0a-5753ca2769f6");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "182fea8b-62d1-4f89-9ca6-ea92294cfbc4", null, "User", "USER" },
                    { "7264453e-a3d4-4495-8d46-0f8ff38f616b", null, "Admin", "ADMIN" }
                });
        }
    }
}
