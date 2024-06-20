using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class acc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_AccountId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Accounts");

            migrationBuilder.EnsureSchema(
                name: "master");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "master");

            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Accounts",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 4, 24, 15, 52, 10, 113, DateTimeKind.Local).AddTicks(1880), new DateTime(2024, 4, 24, 15, 52, 10, 113, DateTimeKind.Local).AddTicks(1888) });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_ParentId",
                table: "Accounts",
                column: "ParentId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_ParentId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "master",
                newName: "Users");

            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 3, 31, 9, 44, 44, 776, DateTimeKind.Local).AddTicks(9891), new DateTime(2024, 3, 31, 9, 44, 44, 776, DateTimeKind.Local).AddTicks(9923) });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_AccountId",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_AccountId",
                table: "Accounts",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }
    }
}
