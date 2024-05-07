using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class permitSecondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolePermissionMappings_RoleId",
                table: "RolePermissionMappings");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 6, 15, 9, 37, 589, DateTimeKind.Local).AddTicks(4846), new DateTime(2024, 5, 6, 15, 9, 37, 589, DateTimeKind.Local).AddTicks(4855) });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_RoleId_PermissionId",
                table: "RolePermissionMappings",
                columns: new[] { "RoleId", "PermissionId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_RolePermissionMappings_RoleId_PermissionId",
                table: "RolePermissionMappings");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 6, 14, 58, 29, 738, DateTimeKind.Local).AddTicks(7111), new DateTime(2024, 5, 6, 14, 58, 29, 738, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_RoleId",
                table: "RolePermissionMappings",
                column: "RoleId");
        }
    }
}
