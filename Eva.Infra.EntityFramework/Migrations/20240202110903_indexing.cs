using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class indexing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMappings",
                table: "UserRoleMappings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserRoleMappings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMappings",
                table: "UserRoleMappings",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleMappings_UserId_RoleId",
                table: "UserRoleMappings",
                columns: new[] { "UserId", "RoleId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoleMappings",
                table: "UserRoleMappings");

            migrationBuilder.DropIndex(
                name: "IX_UserRoleMappings_UserId_RoleId",
                table: "UserRoleMappings");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "UserRoleMappings",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoleMappings",
                table: "UserRoleMappings",
                columns: new[] { "UserId", "RoleId" });
        }
    }
}
