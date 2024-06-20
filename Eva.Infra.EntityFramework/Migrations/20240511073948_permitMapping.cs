using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class permitMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionEndPointMapping",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PermissionId = table.Column<int>(type: "INTEGER", nullable: false),
                    EvaEndPointId = table.Column<int>(type: "INTEGER", nullable: false),
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
                    table.PrimaryKey("PK_PermissionEndPointMapping", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionEndPointMapping_EvaEndPoints_EvaEndPointId",
                        column: x => x.EvaEndPointId,
                        principalTable: "EvaEndPoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionEndPointMapping_Permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "Permissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 11, 11, 9, 48, 685, DateTimeKind.Local).AddTicks(6777), new DateTime(2024, 5, 11, 11, 9, 48, 685, DateTimeKind.Local).AddTicks(6786) });

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEndPointMapping_EvaEndPointId",
                table: "PermissionEndPointMapping",
                column: "EvaEndPointId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEndPointMapping_PermissionId_EvaEndPointId",
                table: "PermissionEndPointMapping",
                columns: new[] { "PermissionId", "EvaEndPointId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionEndPointMapping");

            migrationBuilder.UpdateData(
                schema: "master",
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedOn", "ModifiedOn" },
                values: new object[] { new DateTime(2024, 5, 6, 15, 9, 37, 589, DateTimeKind.Local).AddTicks(4846), new DateTime(2024, 5, 6, 15, 9, 37, 589, DateTimeKind.Local).AddTicks(4855) });
        }
    }
}
