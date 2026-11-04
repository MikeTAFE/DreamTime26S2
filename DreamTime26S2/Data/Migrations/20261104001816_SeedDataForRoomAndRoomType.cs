using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DreamTime26S2.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForRoomAndRoomType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Capacity", "Name" },
                values: new object[,]
                {
                    { 1, 12, "Bibliophile" },
                    { 2, 10, "Inkwell" },
                    { 3, 6, "Novel Nook" }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Code", "RoomTypeId" },
                values: new object[,]
                {
                    { "B01", 1 },
                    { "B02", 1 },
                    { "B03", 1 },
                    { "I01", 2 },
                    { "I02", 2 },
                    { "I03", 2 },
                    { "I04", 2 },
                    { "N01", 3 },
                    { "N02", 3 },
                    { "N03", 3 },
                    { "N04", 3 },
                    { "N05", 3 },
                    { "N06", 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "B01");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "B02");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "B03");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "I01");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "I02");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "I03");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "I04");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N01");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N02");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N03");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N04");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N05");

            migrationBuilder.DeleteData(
                table: "Rooms",
                keyColumn: "Code",
                keyValue: "N06");

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "RoomTypes",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
