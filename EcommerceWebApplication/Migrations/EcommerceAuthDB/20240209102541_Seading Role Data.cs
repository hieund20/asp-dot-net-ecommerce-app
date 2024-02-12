using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecommerce.API.Migrations.EcommerceAuthDB
{
    /// <inheritdoc />
    public partial class SeadingRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55e271b8-1805-4e41-9941-8d0b97779646", "55e271b8-1805-4e41-9941-8d0b97779646", "Writer", "WRITER" },
                    { "6cd46449-0660-421a-9b42-a68377921711", "6cd46449-0660-421a-9b42-a68377921711", "Reader", "READER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55e271b8-1805-4e41-9941-8d0b97779646");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cd46449-0660-421a-9b42-a68377921711");
        }
    }
}
