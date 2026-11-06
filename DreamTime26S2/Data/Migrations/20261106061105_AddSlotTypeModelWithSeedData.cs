using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DreamTime26S2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSlotTypeModelWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlotTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlotTypes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SlotTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Morning" },
                    { 2, "Afternoon" },
                    { 3, "Evening" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SlotTypes_Name",
                table: "SlotTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlotTypes");
        }
    }
}
