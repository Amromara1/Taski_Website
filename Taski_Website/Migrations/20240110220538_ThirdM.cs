using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Taski_Website.Migrations
{
    /// <inheritdoc />
    public partial class ThirdM : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTask");

            migrationBuilder.CreateTable(
                name: "TaskiTaskTaskiUser",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskiTaskTaskiUser", x => new { x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskiTaskTaskiUser_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskiTaskTaskiUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2024, 1, 10, 23, 5, 37, 849, DateTimeKind.Local).AddTicks(1202));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2024, 1, 10, 23, 5, 37, 849, DateTimeKind.Local).AddTicks(1263));

            migrationBuilder.CreateIndex(
                name: "IX_TaskiTaskTaskiUser_UserId",
                table: "TaskiTaskTaskiUser",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskiTaskTaskiUser");

            migrationBuilder.CreateTable(
                name: "UserTask",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TaskId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserTask_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "TaskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTask_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 1,
                column: "DueDate",
                value: new DateTime(2024, 1, 10, 22, 59, 52, 642, DateTimeKind.Local).AddTicks(1162));

            migrationBuilder.UpdateData(
                table: "Tasks",
                keyColumn: "TaskId",
                keyValue: 2,
                column: "DueDate",
                value: new DateTime(2024, 1, 10, 22, 59, 52, 642, DateTimeKind.Local).AddTicks(1227));

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_TaskId",
                table: "UserTask",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTask_UserId",
                table: "UserTask",
                column: "UserId");
        }
    }
}
