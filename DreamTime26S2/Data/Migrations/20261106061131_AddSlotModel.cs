using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DreamTime26S2.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddSlotModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    SlotTypeId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Available = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Slots_SlotTypes_SlotTypeId",
                        column: x => x.SlotTypeId,
                        principalTable: "SlotTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slots_Timeslots_EndTime",
                        column: x => x.EndTime,
                        principalTable: "Timeslots",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Slots_Timeslots_StartTime",
                        column: x => x.StartTime,
                        principalTable: "Timeslots",
                        principalColumn: "Time",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Slots_EndTime",
                table: "Slots",
                column: "EndTime");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_SlotTypeId",
                table: "Slots",
                column: "SlotTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Slots_StartTime",
                table: "Slots",
                column: "StartTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Slots");
        }
    }
}
