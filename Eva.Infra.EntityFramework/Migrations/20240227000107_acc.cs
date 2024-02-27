using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class acc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountId",
                table: "Accounts",
                type: "INTEGER",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
