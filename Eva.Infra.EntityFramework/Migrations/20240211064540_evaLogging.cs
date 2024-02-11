using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class evaLogging : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaLog_Users_UserId",
                table: "EvaLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaLog",
                table: "EvaLog");

            migrationBuilder.RenameTable(
                name: "EvaLog",
                newName: "EvaLogs");

            migrationBuilder.RenameIndex(
                name: "IX_EvaLog_UserId",
                table: "EvaLogs",
                newName: "IX_EvaLogs_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaLogs",
                table: "EvaLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EvaLogs_Users_UserId",
                table: "EvaLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EvaLogs",
                table: "EvaLogs");

            migrationBuilder.RenameTable(
                name: "EvaLogs",
                newName: "EvaLog");

            migrationBuilder.RenameIndex(
                name: "IX_EvaLogs_UserId",
                table: "EvaLog",
                newName: "IX_EvaLog_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EvaLog",
                table: "EvaLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_EvaLog_Users_UserId",
                table: "EvaLog",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
