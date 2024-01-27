using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taski_Website.Migrations
{
    /// <inheritdoc />
    public partial class NewUpdate1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "Tasks");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2024, 1, 23, 22, 20, 54, 768, DateTimeKind.Local).AddTicks(6830));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2024, 1, 23, 22, 20, 54, 768, DateTimeKind.Local).AddTicks(6938));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "Tasks",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                columns: new[] { "DueDate", "TaskStatus" },
                values: new object[] { new DateTime(2024, 1, 10, 23, 8, 39, 864, DateTimeKind.Local).AddTicks(546), "Active" });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                columns: new[] { "DueDate", "TaskStatus" },
                values: new object[] { new DateTime(2024, 1, 10, 23, 8, 39, 864, DateTimeKind.Local).AddTicks(611), "Failed" });
        }
    }
}
