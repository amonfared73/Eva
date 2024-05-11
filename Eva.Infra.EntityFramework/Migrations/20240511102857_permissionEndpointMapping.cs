using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class permissionEndpointMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPointMapping_EvaEndPoints_EvaEndPointId",
                table: "PermissionEndPointMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPointMapping_Permissions_PermissionId",
                table: "PermissionEndPointMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionEndPointMapping",
                table: "PermissionEndPointMapping");

            migrationBuilder.RenameTable(
                name: "PermissionEndPointMapping",
                newName: "PermissionEndPointMappings");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionEndPointMapping_PermissionId_EvaEndPointId",
                table: "PermissionEndPointMappings",
                newName: "IX_PermissionEndPointMappings_PermissionId_EvaEndPointId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionEndPointMapping_EvaEndPointId",
                table: "PermissionEndPointMappings",
                newName: "IX_PermissionEndPointMappings_EvaEndPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionEndPointMappings",
                table: "PermissionEndPointMappings",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 11, 13, 58, 57, 302, DateTimeKind.Local).AddTicks(8712), new DateTime(2024, 5, 11, 13, 58, 57, 302, DateTimeKind.Local).AddTicks(8720) });

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPointMappings_EvaEndPoints_EvaEndPointId",
                table: "PermissionEndPointMappings",
                column: "EvaEndPointId",
                principalTable: "EvaEndPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPointMappings_Permissions_PermissionId",
                table: "PermissionEndPointMappings",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPointMappings_EvaEndPoints_EvaEndPointId",
                table: "PermissionEndPointMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_PermissionEndPointMappings_Permissions_PermissionId",
                table: "PermissionEndPointMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PermissionEndPointMappings",
                table: "PermissionEndPointMappings");

            migrationBuilder.RenameTable(
                name: "PermissionEndPointMappings",
                newName: "PermissionEndPointMapping");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionEndPointMappings_PermissionId_EvaEndPointId",
                table: "PermissionEndPointMapping",
                newName: "IX_PermissionEndPointMapping_PermissionId_EvaEndPointId");

            migrationBuilder.RenameIndex(
                name: "IX_PermissionEndPointMappings_EvaEndPointId",
                table: "PermissionEndPointMapping",
                newName: "IX_PermissionEndPointMapping_EvaEndPointId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PermissionEndPointMapping",
                table: "PermissionEndPointMapping",
                column: "Id");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 11, 11, 9, 48, 685, DateTimeKind.Local).AddTicks(6777), new DateTime(2024, 5, 11, 11, 9, 48, 685, DateTimeKind.Local).AddTicks(6786) });

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPointMapping_EvaEndPoints_EvaEndPointId",
                table: "PermissionEndPointMapping",
                column: "EvaEndPointId",
                principalTable: "EvaEndPoints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PermissionEndPointMapping_Permissions_PermissionId",
                table: "PermissionEndPointMapping",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
