using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFCore_ConsoleApp.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TASK_PRIORITY",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "(Nueva prioridad de tarea sin nombre)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK_PRIORITY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TASK",
                columns: table => new
                {
                    ID = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NAME = table.Column<string>(type: "TEXT", nullable: false, defaultValue: "(Nueva tarea sin nombre)"),
                    DONE = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                    DUE_DATE = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    TASK_PRIORITY_ID = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TASK", x => x.ID);
                    table.ForeignKey(
                        name: "FK_TASK_TASK_PRIORITY_TASK_PRIORITY_ID",
                        column: x => x.TASK_PRIORITY_ID,
                        principalTable: "TASK_PRIORITY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TASK_TASK_PRIORITY_ID",
                table: "TASK",
                column: "TASK_PRIORITY_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TASK");

            migrationBuilder.DropTable(
                name: "TASK_PRIORITY");
        }
    }
}
