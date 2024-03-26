using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Eva.Infra.EntityFramework.Migrations
{
    public partial class uniqueIndexOnEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Inventories_Number",
                table: "Inventories",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_GoodCode",
                table: "Goods",
                column: "GoodCode",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Inventories_Number",
                table: "Inventories");

            migrationBuilder.DropIndex(
                name: "IX_Goods_GoodCode",
                table: "Goods");
        }
    }
}
