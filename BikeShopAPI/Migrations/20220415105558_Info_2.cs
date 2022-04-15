using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BikeShopAPI.Migrations
{
    public partial class Info_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aaa",
                table: "BasketOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Aaa",
                table: "BasketOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
