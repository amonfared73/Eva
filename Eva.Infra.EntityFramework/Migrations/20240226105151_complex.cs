using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class complex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "EvaLogs",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.CreateTable(
                name: "ComplexNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Real = table.Column<double>(type: "REAL", nullable: false),
                    Imaginary = table.Column<double>(type: "REAL", nullable: false),
                    FriendlyState = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexNumbers", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs");

            migrationBuilder.DropTable(
                name: "ComplexNumbers");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "EvaLogs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
