using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WorkingHoursTracker.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingHours_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreationTime", "FirstName", "LastName", "LastUpdate" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 4, 28, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9206), "Tomislav", "Marinković", new DateTime(2024, 4, 28, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9337) },
                    { 2, new DateTime(2024, 4, 25, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9341), "Marina", "Perović", new DateTime(2024, 4, 28, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9345) },
                    { 3, new DateTime(2024, 4, 25, 0, 56, 0, 784, DateTimeKind.Local).AddTicks(9347), "Petra", "Božinović", new DateTime(2024, 4, 25, 0, 56, 0, 784, DateTimeKind.Local).AddTicks(9349) },
                    { 4, new DateTime(2024, 4, 20, 20, 56, 0, 784, DateTimeKind.Local).AddTicks(9351), "Mario", "Došen", new DateTime(2024, 4, 25, 5, 56, 0, 784, DateTimeKind.Local).AddTicks(9353) },
                    { 5, new DateTime(2024, 4, 21, 16, 56, 0, 784, DateTimeKind.Local).AddTicks(9355), "Ozren", "Bogdan", new DateTime(2024, 4, 28, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9357) }
                });

            migrationBuilder.InsertData(
                table: "WorkingHours",
                columns: new[] { "Id", "Duration", "EmployeeId", "EndTime", "StartTime" },
                values: new object[,]
                {
                    { 1, 20L, 1, new DateTime(2024, 4, 28, 23, 56, 0, 784, DateTimeKind.Local).AddTicks(9458), new DateTime(2024, 4, 28, 23, 36, 0, 784, DateTimeKind.Local).AddTicks(9454) },
                    { 2, 10L, 1, new DateTime(2024, 4, 28, 23, 26, 0, 784, DateTimeKind.Local).AddTicks(9464), new DateTime(2024, 4, 28, 23, 16, 0, 784, DateTimeKind.Local).AddTicks(9462) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingHours_EmployeeId",
                table: "WorkingHours",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkingHours");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
