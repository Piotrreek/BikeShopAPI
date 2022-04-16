using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShopAPI.Migrations
{
    public partial class BasketCreatorShopChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopCreatorId",
                table: "Baskets",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopCreatorId",
                table: "Baskets");
        }
    }
}
