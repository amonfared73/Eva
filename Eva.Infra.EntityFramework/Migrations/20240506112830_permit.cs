using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class permit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StateCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionMappings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoleId = table.Column<int>(type: "INTEGER", nullable: false),
                    PermissionId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ModifiedBy = table.Column<int>(type: "INTEGER", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StateCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IsDeleted = table.Column<bool>(type: "INTEGER", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionMappings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionMappings_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 6, 14, 58, 29, 738, DateTimeKind.Local).AddTicks(7111), new DateTime(2024, 5, 6, 14, 58, 29, 738, DateTimeKind.Local).AddTicks(7120) });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_PermissionId",
                table: "RolePermissionMappings",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionMappings_RoleId",
                table: "RolePermissionMappings",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePermissionMappings");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 4, 27, 10, 6, 11, 327, DateTimeKind.Local).AddTicks(3566), new DateTime(2024, 4, 27, 10, 6, 11, 327, DateTimeKind.Local).AddTicks(3574) });
        }
    }
}
